using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NDataflashEditor.Managers;
using NDataflashEditor.UI;
using System.Collections.Generic;
using System.IO;
using NLog;
using NDataflash;
using NDataflashEditor.Models;
using NDataflash.Definition;
using NDataflashEditor.Storages;
using NDataflashEditor.Core;

namespace NDataflashEditor.Windows
{
	internal partial class DeviceConfiguratorWindow : Form
	{
		private const int MinimumWatts = 1;
		private const int MaximumWatts = 250;
		
		private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

		private readonly BackgroundWorker m_worker = new BackgroundWorker { WorkerReportsProgress = true };
		private readonly USBConnector m_usbConnector = new USBConnector();
        private DeviceInfo m_deviceInfo = USBConnector.UnknownDevice;
        private DataflashManager m_manager;
        private readonly DataFlashDefinitionsStorage m_dataFlashDefinitionsStorage = new DataFlashDefinitionsStorage();
        private DataflashDefinition m_dataFlashDefinition;
        private Dataflash m_dataFlash;
		private bool m_isDeviceWasConnectedOnce;
		private bool m_isDeviceConnected;

		public DeviceConfiguratorWindow()
		{
			InitializeComponent();
            InitializeControls();
            Initialize();
        }
        
		private void InitializeControls()
		{
			//var errorIcon = BitmapProcessor.CreateIcon(Resources.exclamation);
			//if (errorIcon != null) MainErrorProvider.Icon = errorIcon;

			MainContainer.SelectedPage = WelcomePage;
		
			DownloadButton.Click += DownloadButton_Click;
			UploadButton.Click += UploadButton_Click;
			ResetButton.Click += ResetButton_Click;
            SaveAsButton.Click += SaveAsButton_Click;

			RestartButton.Click += RestartButton_Click;
			ResetAndRestartButton.Click += ResetAndRestartButton_Click;
		}

        private string GetDataflashFileName()
        {
            return m_deviceInfo.Name;
            //return string.Format("{0} HW v{1} FW v{2}", m_deviceInfo.Name, m_dataFlash.HardwareVersion, m_dataFlash.FirmwareVersion);
        }

        private void SaveAsButton_Click(object sender, EventArgs e)
        {
            string fileName;
            using (var sf = new SaveFileDialog { Title = @"Select dataflash file location ...", Filter = NDataflashEditor.Core.Consts.DataFlashFilter, FileName = GetDataflashFileName() })
            {
                if (sf.ShowDialog() != DialogResult.OK) return;
                fileName = sf.FileName;
                m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker =>
                {
                    try
                    {
                        UpdateUI(() => SetControlButtonsState(false));
                        File.WriteAllBytes(fileName, m_dataFlash.Data);
                    }
                    catch (Exception ex)
                    {
                        InfoBox.Show("An exception occured during save.\n" + ex.Message);
                    }
                    finally
                    {
                        UpdateUI(() => SetControlButtonsState(true));
                    }
                }));
            }          
         }

        private bool ValidateConnectionStatus()
		{
			while (!m_isDeviceConnected)
			{
				var result = InfoBox.Show
				(
					"No compatible USB devices are connected." +
					"\n\n" +
					"To continue, please connect one." +
					"\n\n" +
					"If one already IS connected, try unplugging and plugging it back in. The cable may be loose.",
					MessageBoxButtons.OKCancel
				);
				if (result == DialogResult.Cancel)
				{
					return false;
				}
			}
			return true;
		}

		private void Initialize()
		{
            m_dataFlashDefinitionsStorage.Initialize();

            m_worker.DoWork += Worker_DoWork;
			m_worker.ProgressChanged += (s, e) => ProgressLabel.Text = e.ProgressPercentage + @"%";
			m_worker.RunWorkerCompleted += (s, e) => ProgressLabel.Text = @"Operation completed";

			m_usbConnector.DeviceConnected += DeviceConnected;
			m_usbConnector.StartUSBConnectionMonitoring();
		}

		private void InitializeWorkspaceFromDataflash()
		{

            DataflashDefinitionEditorControl.DataflashManager = m_manager;
            DataflashDefinitionEditorControl.Definition = m_dataFlashDefinition;
            DataflashDefinitionEditorControl.UpdateData();

            DeviceNameLabel.Text = USBConnector.GetDeviceInfo(m_dataFlashDefinition.ProductId).Name;
			//FirmwareVersionTextBox.Text = (m_dataFlash.FirmwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			//HardwareVersionTextBox.Text = (m_dataFlash.HardwareVersion / 100f).ToString("0.00", CultureInfo.InvariantCulture);
			//BootModeTextBox.Text = ((Models.BootMode)m_simple.BootMode).ToString();
            propertyGrid1.SelectedObject = new DataflashObjectDescriptor(m_manager);
        }

		private void ReadDataflash(BackgroundWorker worker = null)
		{
			m_dataFlash = m_usbConnector.ReadDataflash(worker);
            m_dataFlashDefinition = GetDataflashDefinition(m_dataFlash);

            m_deviceInfo = USBConnector.GetDeviceInfo(m_dataFlashDefinition.ProductId);

            

            m_manager = new DataflashManager(m_dataFlash, m_dataFlashDefinition);
		}

        private DataflashDefinition GetDataflashDefinition(Dataflash m_dataflash)
        {
            //TODO : auto detect definition
            return m_dataFlashDefinitionsStorage.LoadAll().Last();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
		{
			var worker = (BackgroundWorker)sender;
			var wrapper = (AsyncProcessWrapper)e.Argument;

			try
			{
				UpdateUI(() => SetControlButtonsState(false));
				wrapper.Processor(worker);
			}
			finally
			{
				UpdateUI(() => SetControlButtonsState(true));
			}
		}

		private void DeviceConnected(bool isConnected)
		{
			m_isDeviceConnected = isConnected;
            UpdateUI(() => StatusLabel.Text = @"Device is " + (m_isDeviceConnected ? "connected" : "disconnected"));

			if (m_isDeviceWasConnectedOnce) return;

			if (!m_isDeviceConnected)
			{
				UpdateUI(() =>
				{
					UpdateUI(() => WelcomeLabel.Text = "Waiting for device with\n\nCompatible Firmware\n\n or newer");
					MainContainer.SelectedPage = WelcomePage;
					m_dataFlash = null;
				});
				return;
			}

			UpdateUI(() => WelcomeLabel.Text = @"Downloading settings...");
			try
			{
				ReadDataflash();
                UpdateUI(() =>
				{
                    InitializeWorkspaceFromDataflash();
					MainContainer.SelectedPage = WorkspacePage;
					m_isDeviceWasConnectedOnce = true;
				}, false);
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex);
				UpdateUI(() => WelcomeLabel.Text = @"Unable to download device settings. Reconnect your device.");
			}
		}

		private void DownloadButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker =>
			{
				try
				{
					ReadDataflash(worker);
					UpdateUI(() => InitializeWorkspaceFromDataflash());
				}
				catch (Exception ex)
				{
					s_logger.Warn(ex);
					InfoBox.Show(GetErrorMessage("downloading settings"));
				}
			}));
		}

		private void UploadButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker =>
			{
				try
				{
					var dataflashCopy = new byte[m_dataFlash.Data.Length];
					Buffer.BlockCopy(m_dataFlash.Data, 0, dataflashCopy, 0, m_dataFlash.Data.Length);
					m_usbConnector.WriteDataflash(new Dataflash { Data = dataflashCopy }, worker);
				}
				catch (Exception ex)
				{
					s_logger.Warn(ex);
					InfoBox.Show(GetErrorMessage("uploading settings"));
				}
			}));
		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			m_worker.RunWorkerAsync(new AsyncProcessWrapper(worker =>
			{
				try
				{
					m_usbConnector.ResetDataflash();
					ReadDataflash(worker);
					UpdateUI(() => InitializeWorkspaceFromDataflash());
				}
				catch (Exception ex)
				{
					s_logger.Warn(ex);
					InfoBox.Show(GetErrorMessage("resetting settings"));
				}
			}));
		}


		private void RestartButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			try
			{
				m_usbConnector.RestartDevice();
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex);
				InfoBox.Show(GetErrorMessage("restarting device"));
			}
		}

		private void ResetAndRestartButton_Click(object sender, EventArgs e)
		{
			if (!ValidateConnectionStatus()) return;

			try
			{
				m_usbConnector.ResetDataflash();
				m_usbConnector.RestartDevice();
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex);
				InfoBox.Show(GetErrorMessage("resetting dataflash and restarting device"));
			}
		}

		private void SetControlButtonsState(bool enabled)
		{
			DownloadButton.Enabled = UploadButton.Enabled = ResetButton.Enabled = RestartButton.Enabled = ResetAndRestartButton.Enabled = enabled;
		}

		private string GetErrorMessage(string operationName)
		{
			return "An error occurred during " +
			       operationName +
			       "...\n\n" +
				   "To continue, please activate or reconnect your device.";
		}

        protected void UpdateUI(Action action, bool supressExceptions = true)
        {
            if (!supressExceptions)
            {
                Invoke(action);
            }
            else
            {
                try
                {
                    Invoke(action);
                }
                catch (Exception)
                {
                    // Ignore
                }
            }
        }
    }
}
