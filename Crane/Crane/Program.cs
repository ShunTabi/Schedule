using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Crane
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Mutex _mutex = new Mutex(false, "Crane.exe");
            if (_mutex.WaitOne(0, false) == false)
            {
                FunMSG.ErrMsg(ConMSG.CheckMSG.message00006);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Crane());
        }
    }
}
