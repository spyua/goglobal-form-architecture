using Dicon.Project.Swtich.Testing.Controller;
using MetroFramework.Forms;
using System;
using System.Windows.Forms;

namespace Dicon.Project.Swtich.Test
{
    public partial class MainForm : MetroForm
    {
        private IRepositoryController _repoController;
        
        public MainForm(IRepositoryController controller)
        {
            _repoController = controller;
            InitializeComponent();
        }

        private void ViewSetting()
        {
            // Full Size Setting
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
        }
        private void LoadViewData()
        {
            ProductLineCheckBox.DataSource = _repoController.QueryProductLine();
            ProductNameCheckBox.DataSource = _repoController.QueryProductName();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //View Init
            ViewSetting();
            LoadViewData();
            //Hardware Init
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            /*
            var viewModel = new TestResult()
            {
                COMP_SN = "124",
                CHANNEL_NO = 10,
                CREATE_DATE = DateTime.Now,
                EEPROM_DATE = DateTime.Now,
                BR_DATE = DateTime.Now
            };
            _repoController.CreateCompTestingReport(viewModel);

            /* For Testing Code
            var entity = new TestResult()
            {
                COMP_SN = "124",
                CHANNEL_NO = 10,
            };
            _controller.CreateCompTestingReport(entity);

            entity.CHANNEL_NO = 100;
            _controller.UpdateComTestingReport(entity);
            */


            //var prod = _controller.QueryProductInfo();

        }


       
    }
}

