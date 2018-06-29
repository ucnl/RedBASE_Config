using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows.Forms;
using UCNLDrivers;
using UCNLNMEA;

namespace RedBASE_Config
{
    public partial class MainForm : Form
    {
        #region Custom enums

        enum AppState
        {
            PORT_ABSENT,
            PORT_OPENED,
            PORT_ERROR,
            PORT_CLOSED,
            DEV_INFO_QUERIED,
            DEV_INFO_RECEIVED,
            DEV_TIMEOUT,            
            WRONG_DEVICE,
            DEV_SETTINGS_QUERIED,
            DEV_SETTINGS_RECEIVED,
            DEV_SETTINGS_APPLY_QUERIED,
            DEV_SETTINGS_UPDATED,
            INVALID
        }

        enum RedBASE_Addr
        {
            REDBASE_ADDR_1 = 0,
            REDBASE_ADDR_2 = 1,
            REDBASE_ADDR_3 = 2,
            REDBASE_ADDR_4 = 3,
            REDBASE_ADDR_INVALID
        }

        enum LOC_DATA_INFO
        {
            LOC_DATA_DEVICE_INFO        = 0,
            LOC_DATA_MAX_SUBSCRIBERS    = 2,
            LOC_DATA_PRESSURE_RATING    = 6,
            LOC_DATA_UNKNOWN
        }

        enum LOC_ERR
        {
            LOC_ERR_NO_ERROR = 0,
            LOC_ERR_INVALID_SYNTAX = 1,
            LOC_ERR_UNSUPPORTED = 2,
            LOC_ERR_TRANSMITTER_BUSY = 3,
            LOC_ERR_ARGUMENT_OUT_OF_RANGE = 4,
            LOC_ERR_INVALID_OPERATION = 5,
            LOC_ERR_UNKNOWN_FIELD_ID = 6,
            LOC_ERR_VALUE_UNAVAILIBLE = 7,
            LOC_ERR_RECEIVER_BUSY = 8,
            LOC_ERR_INVALID
        }

        enum DEV_TYPE
        {
            DEV_REDBASE = 0, 
            DEV_REDNODE = 1, 
            DEV_REDNAV = 2,  
            DEV_REDGTR = 3,  
            DEV_UNKNOWN
        }

        #endregion

        #region Invokers

        #region Properties

        AppState state = AppState.INVALID;
        AppState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                if (this.InvokeRequired)
                    this.Invoke((MethodInvoker)delegate { OnAppStateChanged(state); });
                else
                    OnAppStateChanged(state);
            }
        }

        NMEASerialPort port;
        PrecisionTimer timer;

        int timerTicks = 0;
        int timeoutTicks = 5;

        RedBASE_Addr BuoyAddr
        {
            get
            {
                return (RedBASE_Addr)Enum.Parse(typeof(RedBASE_Addr), buoyAddrCbx.SelectedItem.ToString());
            }
            set
            {
                int idx = buoyAddrCbx.Items.IndexOf(value.ToString());
                if (idx >= 0)
                    buoyAddrCbx.SelectedIndex = idx;
            }
        }

        #endregion

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();

            var portNames = SerialPort.GetPortNames();

            if (portNames.Length > 0)
            {
                #region Other

                buoyAddrCbx.Items.AddRange(Enum.GetNames(typeof(RedBASE_Addr)));
                BuoyAddr = RedBASE_Addr.REDBASE_ADDR_INVALID;

                #endregion

                #region NMEA

                NMEAParser.AddManufacturerToProprietarySentencesBase(ManufacturerCodes.TNT);
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "0", "x");   // ACK $PTNT0,errCode
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "4", "x,x"); // LOC_DATA_GET $PTNT4,dataID,reserved
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "5", "x,x"); // LOC_DATA_VAL $PTNT5,dataID,dataVal
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "!", "c--c,x,c--c,x,x,c--c"); // DEV_INFO $PTNT!,sys_moniker,sys_ver,dev_type,core_moniker,core_ver,dev_serial_num
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "7", "x");   // SETTINGS_GET $PTNT7,reserved
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "8", "x,x"); // SETTINGS_SET $PTNT8,buoyAddr,isInverted
                NMEAParser.AddProprietarySentenceDescription(ManufacturerCodes.TNT, "9", "x,x"); // SETTINGS_VAL $PTNT9,buoyAddr,isInverted

                #endregion

                #region timer

                timer = new PrecisionTimer();
                timer.Period = 1000;
                timer.Mode = Mode.Periodic;
                timer.Tick += new EventHandler(timer_Tick);
                timer.Started += new EventHandler(timer_Started);
                timer.Stopped += new EventHandler(timer_Stopped);

                #endregion

                #region port

                port = new NMEASerialPort(new SerialPortSettings("COM1", BaudRate.baudRate9600, Parity.None, DataBits.dataBits8, StopBits.One, Handshake.None));
                port.NewNMEAMessage += new EventHandler<NewNMEAMessageEventArgs>(port_NewNMEAMessage);
                port.PortError += new EventHandler<SerialErrorReceivedEventArgs>(port_Error);
                port.IsRawModeOnly = false;

                #endregion

                portNameCbx.Items.AddRange(portNames);
                portNameCbx.SelectedIndex = 0;

                State = AppState.PORT_CLOSED;
            }
            else
            {
                State = AppState.PORT_ABSENT;
            }

        }

        #endregion
       
        #region Methods    

        #region Parsers

        private void DEVICE_INFO_Parse(object[] parameters)
        {
            //sys_moniker,sys_ver,core_moniker,core_ver,dev_type,dev_serial_num
            string sys_moniker = (string)parameters[0];
            int sys_ver = (int)parameters[1];            
            string core_moniker = (string)parameters[2];
            int core_ver = (int)parameters[3];
            DEV_TYPE devType = (DEV_TYPE)(int)parameters[4];
            string dev_serial_num = (string)parameters[5];

            serialTxb.Invoke((MethodInvoker)delegate { serialTxb.Text = dev_serial_num; });

            deviceInfoTxb.Invoke((MethodInvoker)delegate
            {
                deviceInfoTxb.Text = string.Format("System: {0} v.{1}, Core: {2} v.{3}, Device: {4}",
                    sys_moniker, BCDVersionToStr(sys_ver), core_moniker, BCDVersionToStr(core_ver), devType);
            });

            if (devType != DEV_TYPE.DEV_REDBASE)
            {
                State = AppState.WRONG_DEVICE;
            }
            else
            {
                State = AppState.DEV_INFO_RECEIVED;
            }
        }

        private void ACK_Parse(object[] parameters)
        {
            LOC_ERR errCode = (LOC_ERR)(int)parameters[0];

            if (errCode == LOC_ERR.LOC_ERR_NO_ERROR)
            {
                if (State == AppState.DEV_SETTINGS_APPLY_QUERIED)
                    State = AppState.DEV_SETTINGS_UPDATED;
            }
            else
            {
                State = AppState.PORT_ERROR;
            }
        }
                  
        private void LOC_DATA_VAL_Parse(object[] parameters)
        {
            //
        }

        private void SETTINGS_VAL_Parse(object[] parameters)
        {
            buoyAddrCbx.Invoke((MethodInvoker)delegate { BuoyAddr = (RedBASE_Addr)(int)parameters[0]; });
            State = AppState.DEV_SETTINGS_RECEIVED;
        }

        #endregion

        public static string BCDVersionToStr(int versionData)
        {
            return string.Format("{0}.{1}", (versionData >> 0x08).ToString(), (versionData & 0xff).ToString("X2"));
        }

        private bool TrySend(string message)
        {
            bool result = false;

            try
            {
                port.SendData(message);
                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        
        private void OnAppStateChanged(AppState state)
        {
            switch (state)
            {
                case AppState.PORT_ABSENT:
                {
                    portNameCbx.Enabled = false;
                    openClosePortBtn.Enabled = false;
                    deviceInfoGroup.Enabled = false;
                    buoyAddressGroup.Enabled = false;
                    statusLbl.Text = "No ports available";
                    break;
                }
                case AppState.PORT_OPENED:
                {
                    portNameCbx.Enabled = false;
                    openClosePortBtn.Text = "Close";
                    deviceInfoGroup.Enabled = false;
                    buoyAddressGroup.Enabled = false;
                    statusLbl.Text = "Port opened";

                    var message = NMEAParser.BuildProprietarySentence(ManufacturerCodes.TNT, "4", new object[] { LOC_DATA_INFO.LOC_DATA_DEVICE_INFO, 0 });
                    if (!TrySend(message))
                        State = AppState.PORT_CLOSED;
                    else
                    {
                        State = AppState.DEV_INFO_QUERIED;
                    }
                    
                    break;
                }
                case AppState.PORT_ERROR:
                {
                    if (timer.IsRunning)
                        timer.Stop();

                    statusLbl.Text = "Port error";
                    break;
                }
                case AppState.PORT_CLOSED:
                {
                    if (timer.IsRunning) 
                        timer.Stop();

                    portNameCbx.Enabled = true;
                    openClosePortBtn.Text = "Open";
                    openClosePortBtn.Enabled = true;                   
                    deviceInfoGroup.Enabled = false;
                    buoyAddressGroup.Enabled = false;

                    statusLbl.Text = "Port closed";
                    break;
                }
                case AppState.DEV_INFO_QUERIED:
                {
                    statusLbl.Text = "Device info queried...";
                    timer.Start();                    
                    break;
                }
                case AppState.DEV_INFO_RECEIVED:
                {
                    if (timer.IsRunning)
                        timer.Stop();

                    deviceInfoGroup.Enabled = true;                    
                    statusLbl.Text = "Device info received";

                    string message = NMEAParser.BuildProprietarySentence(ManufacturerCodes.TNT, "7", new object[] { 0 });

                    if (TrySend(message))
                    {
                        State = AppState.DEV_SETTINGS_QUERIED;
                        timer.Start();       
                    }
                    else
                    {
                        State = AppState.PORT_ERROR;
                    }

                    break;
                }
                case AppState.DEV_TIMEOUT:
                {
                    statusLbl.Text = "Device timeout";
                    break;
                }
                case AppState.WRONG_DEVICE:
                {
                    if (timer.IsRunning)
                        timer.Stop();

                    buoyAddressGroup.Enabled = false;
                    statusLbl.Text = "Connected device is not a RedBASE";
                    break;
                }
                case AppState.DEV_SETTINGS_QUERIED:
                {                    
                    statusLbl.Text = "Settings queried...";
                    break;
                }
                case AppState.DEV_SETTINGS_RECEIVED:
                {
                    if (timer.IsRunning)
                        timer.Stop();

                    buoyAddressGroup.Enabled = true;
                    statusLbl.Text = "Settings received";
                    break;
                }
                case AppState.DEV_SETTINGS_APPLY_QUERIED:
                {
                    statusLbl.Text = "Settings apply queried...";
                    timer.Start();
                    break;
                }
                case AppState.DEV_SETTINGS_UPDATED:
                {
                    if (timer.IsRunning)
                        timer.Stop();

                    statusLbl.Text = "Settings updated";
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        

        #endregion

        #region Handlers

        #region port

        private void port_NewNMEAMessage(object sender, NewNMEAMessageEventArgs e)
        {
            try
            {
                var parseResult = NMEAParser.Parse(e.Message);

                if (parseResult is NMEAProprietarySentence)
                {
                    NMEAProprietarySentence sentence = (parseResult as NMEAProprietarySentence);

                    if (sentence.Manufacturer == ManufacturerCodes.TNT)
                    {
                        if (sentence.SentenceIDString == "!")
                        {
                            // DEVICE INFO
                            DEVICE_INFO_Parse(sentence.parameters);
                        }
                        else if (sentence.SentenceIDString == "0")
                        {
                            // ACK
                            ACK_Parse(sentence.parameters);
                        }
                        else if (sentence.SentenceIDString == "5")
                        {
                            // LOC_DATA_VAL
                            LOC_DATA_VAL_Parse(sentence.parameters);
                        }
                        else if (sentence.SentenceIDString == "9")
                        {
                            // SETTINGS_VAL
                            SETTINGS_VAL_Parse(sentence.parameters);
                        }
                    }
                    else
                    {
                        // WTF?
                    }
                }             
            }
            catch (Exception ex)
            {
                // WTF?
            }
        }

        private void port_Error(object sender, SerialErrorReceivedEventArgs e)
        {
            State = AppState.PORT_ERROR;
        }

        #endregion

        #region timer

        private void timer_Tick(object sender, EventArgs e)
        {
            if (timerTicks++ > timeoutTicks)
                timer.Stop();
        }

        private void timer_Started(object sender, EventArgs e)
        {
            timerTicks = 0;
        }

        private void timer_Stopped(object sender, EventArgs e)
        {
            if (timerTicks >= timeoutTicks)
                State = AppState.DEV_TIMEOUT;
        }

        #endregion

        #region UI

        private void openClosePortBtn_Click(object sender, EventArgs e)
        {
            if (port.IsOpen)
            {
                try
                {
                    port.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                State = AppState.PORT_CLOSED;
            }
            else
            {
                try
                {
                    port.PortName = portNameCbx.SelectedItem.ToString();
                    port.Open();

                    State = AppState.PORT_OPENED;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void applyBuoyAddrBtn_Click(object sender, EventArgs e)
        {
            string message = NMEAParser.BuildProprietarySentence(ManufacturerCodes.TNT, "8", new object[] { BuoyAddr, 0 });

            if (TrySend(message))
            {
                State = AppState.DEV_SETTINGS_APPLY_QUERIED;
            }
            else
            {
                State = AppState.PORT_ERROR;
            }
        }

        private void webLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(webLink.Text);
        }

        #endregion        

        #endregion
    }
}
