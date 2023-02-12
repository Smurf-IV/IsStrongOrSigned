using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

using Krypton.Toolkit;

using Signature = System.Management.Automation.Signature;

namespace IsStrongOrSigned
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
            lstLog.Items.Add("Logging will eventually appear here!");
            lstLog.Items.Add("Double-Click a row to open the 'Explorer File Properties' dialog");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            btnAdd_Click(sender, e);
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            using var sampleVistaOpenFileDialog = new Ookii.Dialogs.WinForms.VistaOpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = true,
                Title = @"Select required files",
                Filter = @"All files (*.*)|*.*" // TODO: restrict to `*.exe`, and `*.dll`
            };
            if (sampleVistaOpenFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Enabled = false;
                // Show a modeless dialog; this is the recommended mode of operation for a progress dialog.
                progressDialog1.Show(sampleVistaOpenFileDialog.FileNames);
            }
        }

        private void ProgressDialog_DoWork(object sender, DoWorkEventArgs eventArgs)
        {
            try
            {
                if (eventArgs.Argument is not string[] fileNames)
                {
                    return;
                }

                float step = 100.0f / (fileNames.Length + 1);
                float curStep = step;
                foreach (var fileName in fileNames)
                {
                    // ReportProgress can also modify the main text and description; pass null to leave them unchanged.
                    // If _sampleProgressDialog.ShowTimeRemaining is set to true, the time will automatically be calculated based on
                    // the frequency of the calls to ReportProgress.
                    progressDialog1.ReportProgress((int)(curStep += step));

                    var fields = ProcessFile(fileName);
                    Invoke((MethodInvoker)delegate { dgvFileProperties.Rows.Add(fields); });
                    // Periodically check CancellationPending and abort the operation if required.
                    if (progressDialog1.CancellationPending)
                    {
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                // TODO: Log this out
                Console.WriteLine(e);
            }
        }

        private void progressDialog1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                Enabled = true;
                btnRemovelAll.Enabled = dgvFileProperties.Rows.Count > 0;
                dgvFileProperties.ClearSelection();
            });
        }

        private static string[] ProcessFile(string fileName)
        {
            var fields = new List<string>();
            try
            {
                var fi = new FileInfo(fileName);
                //this.FileName,
                fields.Add(fi.Name);

                //this.NetVersion,
                var (netVersion, compileType) = GetNetVersion(fi.FullName);
                fields.Add(netVersion);
                //this.Type,
                fields.Add(compileType);
                //this.StrongNamed,
                var (strongNamed, strongVerified) = StrongNameSignatureVerification(fi.FullName);
                fields.Add(strongNamed);
                fields.Add(strongVerified);
                var (copyright, binVersion) = GetCopyrightAndVersion(fi.FullName);
                fields.Add(copyright);
                fields.Add(binVersion);
                //this.DigStatus,
                fields.Add(VerifyAuthenticodeSignature(fi.FullName));

                //this.FullPath});
                fields.Add(fi.FullName);
            }
            catch (Exception e)
            {
                // TODO: Log this out
                Console.WriteLine(e);
            }
            return fields.ToArray();
        }

        private static (string copyright, string binVersion) GetCopyrightAndVersion(string fullName)
        {
            try
            {
                var verInfo = FileVersionInfo.GetVersionInfo(fullName);

                string binVersion = verInfo.SpecialBuild;
                if (string.IsNullOrWhiteSpace(binVersion))
                {
                    binVersion = verInfo.FileVersion;
                }

                if (!string.IsNullOrWhiteSpace(verInfo.ProductVersion))
                {
                    // Do not replace with the spaces version
                    var prodVer = verInfo.ProductVersion.Replace(@", ", "");
                    if (binVersion.Length < prodVer.Length)
                    {
                        // in c# assemblies, the "Special build" is done via the `AssemblyInformationalVersion` attribute
                        binVersion = prodVer;
                    }
                }

                return (verInfo.LegalCopyright, binVersion);
            }
            catch
            {
                return ("", "");
            }
        }

        private static (string netVersion, string compileType) GetNetVersion(string fullName)
        {
            try
            {
                var assembly = Assembly.ReflectionOnlyLoadFrom(fullName);
                assembly.ManifestModule.GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine imageFileMachine);
                //// May be less than .net 4
                return (assembly.ImageRuntimeVersion, $@"{peKind}:{imageFileMachine}");
            }
            catch
            {
                return DLLExportLister.GetCrtVersion(fullName);
            }
        }

        /// <summary>
        /// Check for Authenticode Signature
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string VerifyAuthenticodeSignature(string fileName)
        {
            bool isSigned = false;

            try
            {
                try
                {
                    Assembly assembly = Assembly.ReflectionOnlyLoadFrom(fileName);
                    Module module = assembly.GetModules().First();
                    X509Certificate certificate = module.GetSignerCertificate();
                    if (certificate != null)
                    {
                        isSigned = true;
                        var dateString = certificate.GetExpirationDateString();
                        if (DateTime.TryParse(dateString, out var dt))
                        {
                            dateString = dt.ToString(@"u");
                        }
                        return $@"{dateString} [{certificate.Subject}]";
                    }
                }
                catch
                {
                    RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
                    Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration);
                    runspace.Open();

                    Pipeline pipeline = runspace.CreatePipeline();
                    pipeline.Commands.AddScript($@"Get-AuthenticodeSignature ""{fileName}""");

                    Collection<PSObject> results = pipeline.Invoke();
                    runspace.Close();
                    if (results[0].BaseObject is Signature { SignerCertificate: { } } signature)
                    {
                        isSigned = true;
                        return $@"{signature.SignerCertificate.NotAfter:u} [{signature.SignerCertificate.Subject}]";
                    }
                }
            }
            catch (Exception e)
            {
                // TODO: Log this out
                Console.WriteLine(e);
            }

            return $@"{isSigned} [unknown]";
        }


        [DllImport("mscoree.dll", CharSet = CharSet.Unicode)]
        private static extern bool StrongNameSignatureVerificationEx(string wszFilePath, bool fForceVerification, ref bool pfWasVerified);

        private static (string strongNamed, string strongVerified) StrongNameSignatureVerification(string fileName)
        {
            bool pfWasVerified = false;
            return (StrongNameSignatureVerificationEx(fileName, true, ref pfWasVerified).ToString(), pfWasVerified.ToString());
        }

        private void dgvFileProperties_SelectionChanged(object sender, EventArgs e)
        {
            btnRemoveSelected.Enabled = dgvFileProperties.SelectedRows.Count > 0;
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvFileProperties.SelectedRows)
            {
                dgvFileProperties.Rows.Remove(row);
            }
            dgvFileProperties.ClearSelection();
        }

        private void btnRemovelAll_Click(object sender, EventArgs e)
        {
            dgvFileProperties.ClearSelection();
            dgvFileProperties.Rows.Clear();
            btnRemovelAll.Enabled = false;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHELLEXECUTEINFO
        {
            public int cbSize;
            public uint fMask;
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpVerb;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpFile;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpParameters;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpDirectory;
            public int nShow;
            public IntPtr hInstApp;
            public IntPtr lpIDList;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpClass;
            public IntPtr hkeyClass;
            public uint dwHotKey;
            public IntPtr hIcon;
            public IntPtr hProcess;
        }

        private const int SW_SHOW = 5;
        private const uint SEE_MASK_INVOKEIDLIST = 12;
        private void dgvFileProperties_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string fullPath = (string)dgvFileProperties.SelectedRows[0].Cells[@"FullPath"].Value;
            SHELLEXECUTEINFO info = new SHELLEXECUTEINFO();
            info.cbSize = Marshal.SizeOf(info);
            info.lpVerb = @"properties";
            info.lpFile = fullPath;
            info.nShow = SW_SHOW;
            info.fMask = SEE_MASK_INVOKEIDLIST;
            ShellExecuteEx(ref info);
        }
    }
}