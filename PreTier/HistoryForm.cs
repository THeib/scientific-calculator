
using System.Linq;
using System.Windows.Forms;
using Scientific_Calculator.AppTier;


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
            Logger logger = Logger.Instance;
            listBoxLogs.Items.AddRange(logger.getLogs().getEntries().ToArray());

        }
    }
}
