using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Management;
using System.Collections;
using System.Timers;

namespace _LASH____Injector
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        ///////////////////////////////////ПЕРЕМЕННЫЕ///////////////////////////////////
        string GDYFJBQEVV = "[ОШИБКА]";
        string EOARGVTKBO = "[ВЫ НЕ ВЫБРАЛИ ПРОЦЕСС!]";
        string HLASCLZXLB = "Injecting..";
        string GQSHFHJBUA = "[УСПЕШНО!]";
        string OCXZUZVLOW = "DLL (*.dll)|*.dll" + "|All files (*.*)|*.*";
        uint XPHOXUCHSW = 0x1F0FFF;
        string NXBXOWAHEB = "[ПРОЦЕСС НЕ ДОСТУПЕН!]";
        string SEQMNZVLBY = "[WARFACE НЕ ЗАПУЩЕН]";
        string MONREKATQN = "[CS:GO НЕ ЗАПУЩЕН]";
        ///////////////////////////////////ПЕРЕМЕННЫЕ///////////////////////////////////

        [DllImport("kernel32")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, UIntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out IntPtr lpThreadId);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, Int32 dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern UIntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, string lpBuffer, UIntPtr nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        internal static extern Int32 WaitForSingleObject(IntPtr handle, Int32 milliseconds);

        public Int32 GetProcessId(String proc)
        {
            Process[] ProcList;
            ProcList = Process.GetProcessesByName(proc);
            return ProcList[0].Id;
        }

        public void HRE_IjDLL_HYFD(IntPtr NHRE_HPROCESS_ID_GGHD, String DQEV_DLL_GLES) //INJECT DLL
        {
            try
            {
                IntPtr bytesout;
                Int32 YHF_LENWrite_FDUHF = DQEV_DLL_GLES.Length + 1;
                IntPtr FHWEH_AllOCMem_R2G7T = (IntPtr)VirtualAllocEx(NHRE_HPROCESS_ID_GGHD, (IntPtr)null, (uint)YHF_LENWrite_FDUHF, 0x1000, 0x40);
                WriteProcessMemory(NHRE_HPROCESS_ID_GGHD, FHWEH_AllOCMem_R2G7T, DQEV_DLL_GLES, (UIntPtr)YHF_LENWrite_FDUHF, out bytesout);
                UIntPtr HUFD_Injector_FHDG = (UIntPtr)GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

                if (HUFD_Injector_FHDG == null)
                {
                    return;
                }
                IntPtr UDUHF_hThread_FUDU = (IntPtr)CreateRemoteThread(NHRE_HPROCESS_ID_GGHD, (IntPtr)null, 0, HUFD_Injector_FHDG, FHWEH_AllOCMem_R2G7T, 0, out bytesout);
                if (UDUHF_hThread_FUDU == null)
                {
                    return;
                }
                int Result = WaitForSingleObject(UDUHF_hThread_FUDU, 10 * 1000);
                if (Result == 0x00000080L || Result == 0x00000102L || Result == 0xFFFFFFFF)
                {
                    if (UDUHF_hThread_FUDU != null)
                    {
                        CloseHandle(UDUHF_hThread_FUDU);
                    }
                    return;
                }
                Thread.Sleep(1000);
                VirtualFreeEx(NHRE_HPROCESS_ID_GGHD, FHWEH_AllOCMem_R2G7T, (UIntPtr)0, 0x8000);
                if (UDUHF_hThread_FUDU != null)
                {
                    CloseHandle(UDUHF_hThread_FUDU);
                }
                //File.Delete(NEW_DLL_LDFUHU);
                return;
            }
            catch
            {
                MessageBox.Show(GDYFJBQEVV, HLASCLZXLB, MessageBoxButtons.OK, MessageBoxIcon.Error); //ERROR
            }
        }

        //INJECT ALL

        private void button2_Click(object sender, EventArgs e)//ВЫБРАТЬ DLL
        {
            try
            {
                var FILE_DIALOG_JDSIBF = openFileDialog1;
                FILE_DIALOG_JDSIBF.Filter = OCXZUZVLOW;
                if (FILE_DIALOG_JDSIBF.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = "Имя: " + FILE_DIALOG_JDSIBF.SafeFileName;
                    textBox2.Text = "Размер: " + (fjhw_GET_FILE_SIZE_fw(new System.IO.FileInfo(FILE_DIALOG_JDSIBF.FileName)));
                }
            }
            catch
            {
                MessageBox.Show(GDYFJBQEVV, HLASCLZXLB, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)//ВНЕДРИТЬ DLL
        {
            try
            {
                progressBar1.Value = 0;//DLL FILE
                string DQEV_DLL_GLES = openFileDialog1.FileName;
                /*Random rnd = new Random();
                int value = rnd.Next(5000, 20000);
                string GGGG_DLL_GLES = value + ".dll";
                File.Copy(DQEV_DLL_GLES, GGGG_DLL_GLES);*/

                string GJHD_PROCESS_ABUD = comboBox1.SelectedItem.ToString(); //PROCESS
                Int32 NHRE_HPROCESS_ID_GGHD = GetProcessId(GJHD_PROCESS_ABUD); //H_PROCESS
                if (NHRE_HPROCESS_ID_GGHD >= 0)
                {
                    progressBar1.Value = +100;

                    IntPtr JAHD_PR_ID_BJDS = (IntPtr)OpenProcess(XPHOXUCHSW, 1, NHRE_HPROCESS_ID_GGHD);
                    if (JAHD_PR_ID_BJDS == null)
                    {
                        return;
                    }
                    else
                    {
                        HRE_IjDLL_HYFD(JAHD_PR_ID_BJDS, DQEV_DLL_GLES);
                        MessageBox.Show(GQSHFHJBUA, HLASCLZXLB, MessageBoxButtons.OK, MessageBoxIcon.Asterisk); //УСПЕХ
                        progressBar1.Value = 0;
                        if (checkBox2.Checked)
                        {
                            //timer1.Interval = 30000; //30sec
                            timer1.Interval = 1800000; //30min
                            Process proc = Process.GetProcessesByName(comboBox1.Text)[0];
                            timer1.Enabled = true;
                        }
                        if (checkBox1.Checked)
                        {
                            Close();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(EOARGVTKBO, HLASCLZXLB, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public string fjhw_GET_FILE_SIZE_fw(System.IO.FileInfo file)
        {
            try
            {
                double sizeinbytes = file.Length;
                double sizeinkbytes = Math.Round((sizeinbytes / 1024));
                double sizeinmbytes = Math.Round((sizeinkbytes / 1024));
                double sizeingbytes = Math.Round((sizeinmbytes / 1024));
                if (sizeingbytes > 1)
                    return string.Format("{0} GB", sizeingbytes);
                else if (sizeinmbytes > 1)
                    return string.Format("{0} MB", sizeinmbytes);
                else if (sizeinkbytes > 1)
                    return string.Format("{0} KB", sizeinkbytes);
                else
                    return string.Format("{0} B", sizeinbytes);
            }
            catch
            {
                return "Ошибка получения размера файла..";
            }
        }

        private void comboBox1_MouseClick_1(object sender, MouseEventArgs e)
        {
            try
            {
                comboBox1.Items.Clear();
                foreach (Process clsProcess in Process.GetProcesses())
                {
                    comboBox1.Items.Add(clsProcess.ProcessName);
                }
            }
            catch
            {
                MessageBox.Show(GDYFJBQEVV, HLASCLZXLB, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                Process proc = Process.GetProcessesByName(comboBox1.Text)[0];
                textBox3.Text = "PID: " + "" + proc.Id;
                textBox4.Text = "Время старта: " + "" + proc.StartTime;
                textBox5.Text = "Имя процесса: " + "" + proc.ProcessName;
                textBox6.Text = "Имя окна: " + "" + proc.MainWindowTitle;
            }
            catch
            {
                textBox3.Text = "PID: " + "NONE";
                textBox4.Text = "Время старта: " + "NONE";
                textBox5.Text = "Имя процесса: " + "NONE";
                textBox6.Text = "Имя окна: " + "NONE";
            }
        }

        //INJECT WARFACE
        private void button4_Click_1(object sender, EventArgs e)//ВНЕДРИТЬ DLL
        {
            try
            {
                progressBar2.Value = 0;
                string DQEV_DLL_wf_GLES = openFileDialog2.FileName; //DLL FILE
                string GJHD_PROCESS_wf_ABUD = "Game"; //PROCESS
                Int32 NHRE_HPROCESS_ID_wf_GGHD = GetProcessId(GJHD_PROCESS_wf_ABUD); //H_PROCESS
                if (NHRE_HPROCESS_ID_wf_GGHD >= 0)
                {
                    progressBar2.Value = +100;
                    IntPtr JAHD_PR_ID_BJDS = (IntPtr)OpenProcess(XPHOXUCHSW, 1, NHRE_HPROCESS_ID_wf_GGHD);
                    if (JAHD_PR_ID_BJDS == null)
                    {
                        return;
                    }
                    else
                    {
                        HRE_IjDLL_HYFD(JAHD_PR_ID_BJDS, DQEV_DLL_wf_GLES);
                        MessageBox.Show(GQSHFHJBUA, HLASCLZXLB, MessageBoxButtons.OK, MessageBoxIcon.Asterisk); //УСПЕХ
                        progressBar2.Value = 0;
                        if (checkBox2_wf.Checked)
                        {
                            //timer2.Interval = 30000; //30sec
                            timer2.Interval = 1800000; //30min
                            timer2.Enabled = true;
                        }
                        if (checkBox1_wf.Checked)
                        {
                            Close();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(SEQMNZVLBY, HLASCLZXLB, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)//ВЫБРАТЬ DLL
        {
            try
            {
                var FILE_DIALOG_WF_JDSIBF = openFileDialog2;
                openFileDialog2.Filter = OCXZUZVLOW;
                if (FILE_DIALOG_WF_JDSIBF.ShowDialog() == DialogResult.OK)
                {
                    textBox1_wf.Text = "Имя: " + FILE_DIALOG_WF_JDSIBF.SafeFileName;
                    textBox2_wf.Text = "Размер: " + (fjhw_GET_FILE_SIZE_wf_fw(new System.IO.FileInfo(FILE_DIALOG_WF_JDSIBF.FileName)));
                }
            }
            catch
            {
                MessageBox.Show(GDYFJBQEVV, HLASCLZXLB, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string fjhw_GET_FILE_SIZE_wf_fw(System.IO.FileInfo file)
        {
            try
            {
                double sizeinbytes = file.Length;
                double sizeinkbytes = Math.Round((sizeinbytes / 1024));
                double sizeinmbytes = Math.Round((sizeinkbytes / 1024));
                double sizeingbytes = Math.Round((sizeinmbytes / 1024));
                if (sizeingbytes > 1)
                    return string.Format("{0} GB", sizeingbytes);
                else if (sizeinmbytes > 1)
                    return string.Format("{0} MB", sizeinmbytes);
                else if (sizeinkbytes > 1)
                    return string.Format("{0} KB", sizeinkbytes);
                else
                    return string.Format("{0} B", sizeinbytes);
            }
            catch
            {
                return "Ошибка получения размера файла..";
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Process CV_proc_LDO = Process.GetProcessesByName("Game")[0];
            CV_proc_LDO.Kill();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Process fshu_proc_huwf = Process.GetProcessesByName("Game")[0];
                textBox3_wf.Text = "PID: " + "" + fshu_proc_huwf.Id;
                textBox4_wf.Text = "Время старта: " + "" + fshu_proc_huwf.StartTime;
                textBox6_wf.Text = "Имя окна: " + "" + fshu_proc_huwf.MainWindowTitle;
            }
            catch
            {
                textBox3_wf.Text = "PID: " + "НЕ ЗАПУЩЕН";
                textBox4_wf.Text = "Время старта: " + "НЕ ЗАПУЩЕН";
                textBox6_wf.Text = "Имя окна: " + "НЕ ЗАПУЩЕН";
            }
        }
        //INJECT CS:GO
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                var FILE_DIALOG_CS_JDSIBF = openFileDialog3;
                openFileDialog3.Filter = OCXZUZVLOW;
                if (FILE_DIALOG_CS_JDSIBF.ShowDialog() == DialogResult.OK)
                {
                    textBox1_cs.Text = "Имя: " + FILE_DIALOG_CS_JDSIBF.SafeFileName;
                    textBox2_cs.Text = "Размер: " + (fjhw_GET_FILE_SIZE_cs_fw(new System.IO.FileInfo(FILE_DIALOG_CS_JDSIBF.FileName)));
                }
            }
            catch
            {
                MessageBox.Show(GDYFJBQEVV, HLASCLZXLB, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string fjhw_GET_FILE_SIZE_cs_fw(System.IO.FileInfo file)
        {
            try
            {
                double sizeinbytes = file.Length;
                double sizeinkbytes = Math.Round((sizeinbytes / 1024));
                double sizeinmbytes = Math.Round((sizeinkbytes / 1024));
                double sizeingbytes = Math.Round((sizeinmbytes / 1024));
                if (sizeingbytes > 1)
                    return string.Format("{0} GB", sizeingbytes);
                else if (sizeinmbytes > 1)
                    return string.Format("{0} MB", sizeinmbytes);
                else if (sizeinkbytes > 1)
                    return string.Format("{0} KB", sizeinkbytes);
                else
                    return string.Format("{0} B", sizeinbytes);
            }
            catch
            {
                return "Ошибка получения размера файла..";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar3.Value = 0;
                string DQEV_DLL_cs_GLES = openFileDialog3.FileName; //DLL FILE
                string GJHD_PROCESS_cs_ABUD = "csgo"; //PROCESS
                Int32 NHRE_HPROCESS_ID_cs_GGHD = GetProcessId(GJHD_PROCESS_cs_ABUD); //H_PROCESS
                if (NHRE_HPROCESS_ID_cs_GGHD >= 0)
                {
                    progressBar3.Value = +100;
                    IntPtr JAHD_PR_ID_BJDS = (IntPtr)OpenProcess(XPHOXUCHSW, 1, NHRE_HPROCESS_ID_cs_GGHD);
                    if (JAHD_PR_ID_BJDS == null)
                    {
                        return;
                    }
                    else
                    {
                        HRE_IjDLL_HYFD(JAHD_PR_ID_BJDS, DQEV_DLL_cs_GLES);
                        MessageBox.Show(GQSHFHJBUA, HLASCLZXLB, MessageBoxButtons.OK, MessageBoxIcon.Asterisk); //УСПЕХ
                        progressBar3.Value = 0;
                        if (checkBox2_cs.Checked)
                        {
                            //timer3.Interval = 30000; //30sec
                            timer3.Interval = 1800000; //30min
                            timer3.Enabled = true;
                        }
                        if (checkBox1_cs.Checked)
                        {
                            Close();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(MONREKATQN, HLASCLZXLB, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Process FJIF_proc_FUD = Process.GetProcessesByName("csgo")[0];
            FJIF_proc_FUD.Kill();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Process FGFB_proc_FIJDF = Process.GetProcessesByName("csgo")[0];
                textBox3_cs.Text = $"Имя процесса: csgo.exe";
                textBox4_cs.Text = $"PID: {FGFB_proc_FIJDF.Id}";
                textBox5_cs.Text = $"Время старта: {FGFB_proc_FIJDF.StartTime}";
                textBox6_cs.Text = $"Имя окна: {FGFB_proc_FIJDF.MainWindowTitle}";
            }
            catch
            {
                textBox3_cs.Text = "Имя процесса: " + "csgo.exe";
                textBox4_cs.Text = "PID: " + "НЕ ЗАПУЩЕН";
                textBox5_cs.Text = "Время старта: " + "НЕ ЗАПУЩЕН";
                textBox6_cs.Text = "Имя окна: " + "НЕ ЗАПУЩЕН";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("[LASH] - Injector:\n" +
                "- Это:\n" +
                "1)Программа для внедрения dll файла в процесс.\n" +
                "2)Проверка процесса.\n" +
                "3)Запись данных о процессе.\n" +
                "Рекомендованные требования:\n" +
                "Windows 7 / 8 / 8.1 / 10\n" +
                "[32bit / 64bit]\n" +
                "ВНИМАНИЕ! ЕСЛИ ВЫ ИНЖЕКТИТЕ 32BIT DLL,\n" +
                "ТО ЗАПУСКАЙТЕ 32BIT ВЕРСИЮ ДАННОГО СОФТА!\n", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            MessageBox.Show("[LASH] - Injector v1.0\n" +
                "BETA-TEST : USER_1L", "Version", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        //КОНЕЦ INFO
        //ПОСЛЕ ВНЕДРЕНИЯ

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process proc = Process.GetProcessesByName(comboBox1.Text)[0];
            proc.Kill();
        }

        private void timer_st_Tick(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Items.Clear();
                foreach (Process clsProcess in Process.GetProcesses())
                {
                    comboBox1.Items.Add(clsProcess.ProcessName + clsProcess.Id + clsProcess.StartTime + clsProcess.MainWindowTitle);
                }
            }
            catch
            {
                MessageBox.Show(GDYFJBQEVV, HLASCLZXLB, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}