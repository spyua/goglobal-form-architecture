using System;
using System.Windows.Forms;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Threading;
using System.Linq;
using DiconInstrument.Core.Def;
using DiconInstrument.GpibInstrument;
using Dicon.Util;

namespace DiconInstrument.FormTest
{
    public partial class MainForm : Form
    {
        public GpibBoard GPIBBoard { get; private set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GPIBBoard = new GpibBoard();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            GpibDevicesTreeView.Nodes.Add("GPIB Devices");

            try
            {
                GPIBBoard.Connect();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Scan_Click(object sender, EventArgs e)
        {
            // Rx Method-> Scan ManFrame -> Scan Slot Device
            var ob = GPIBBoard.ScanMainFrames(3000)
                     // 掃到ManFrame後走訪
                     .SelectMany(x => x)
                     // 走訪每一個MainFrame 掃描Slot
                     .SelectMany(x => x.ScanSlots(3000))
                     // 改變目前執行的 thread
                     .SubscribeOn(Scheduler.Default)
                     // 設定執行完後回到哪一條Thread (回UI Thread)
                     .ObserveOn(SynchronizationContext.Current)
                     .DoOnSubscribe(() => { 
                         // Disabel Button
                         BtnScanRx.Enabled = false;
                     })
                     .Subscribe(
                           _ =>
                           {
                               // Next Event (此範例無用到此事件)
                           },
                           ex =>
                           {
                               // Error Event
                               BtnScanRx.Enabled = true;
                               MessageBox.Show(ex.Message.ToString());
                           },

                           () =>
                           {
                               // Complete Event
                                MessageBox.Show("Scan Complete");
                               BtnScanRx.Enabled = true;

                               // Update View
                               UpdateDeviceTreeView(GPIBBoard);

                           });
        }
        private void UpdateDeviceTreeView(GpibBoard gpiobard)
        {
            int cnt = 0;
            GpibDevicesTreeView.Nodes[0].Nodes.Clear();

            foreach (var mainFrame in gpiobard.MainFrames)
            {
                GpibDevicesTreeView.Nodes[0].Nodes.Add(mainFrame.Value.DeviceModel.DeviceInformation);

                foreach(var slotDevie in mainFrame.Value.SlotDevices)
                {
                    GpibDevicesTreeView.Nodes[0].Nodes[cnt].Nodes.Add(slotDevie.Value.DeviceModel.DeviceInformation);
                }

                cnt++;
            }

        }

        private async void BtnScanNormal_Click(object sender, EventArgs e)
        {
            // Normal Method
            try
            {
                var gpibDevices = await GPIBBoard.ScanGpibDevMainFrames(1000);
                int cnt = 0;
                GpibDevicesTreeView.Nodes[0].Nodes.Clear();
                foreach (MainFrame mainFrame in gpibDevices)
                {

                    GpibDevicesTreeView.Nodes[0].Nodes.Add(mainFrame.DeviceModel.DeviceInformation);
                    mainFrame.Connect();
                    if (InstrumentDevices.Instance.MainFrame.Contains(mainFrame.DeviceModel.Model))
                    {
                        var slotDevices = await mainFrame.ScanSlotDevices(3000);

                        for (int i = 0; i < slotDevices.Count(); i++)
                        {
                            var device = slotDevices.ToList()[i].DeviceModel;
                            GpibDevicesTreeView.Nodes[0].Nodes[cnt].Nodes.Add(device.DeviceInformation);
                        }
                    }

                    cnt++;

                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
