using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Scientific_Calculator.DataTier;

namespace Scientific_Calculator.PreTier
{
    public partial class HistoryForm : Form
    {
        public HistoryForm()
        {
            InitializeComponent();
            updateLogsList();
        }

        public void updateLogsList()
        {

            listBoxLogs.Items.AddRange(LoggerState.entities.ToArray());

        }
    }
}
