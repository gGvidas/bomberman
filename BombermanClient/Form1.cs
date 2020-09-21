using Microsoft.AspNetCore.SignalR.Client;
using System.Windows.Forms;

namespace BombermanClient
{
    public partial class Form1 : Form
    {
        private int[][] world;
        private HubConnection hubConnection;
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (hubConnection != null)
            {
                if (keyData == Keys.A || keyData == Keys.S || keyData == Keys.D || keyData == Keys.W)
                {
                    hubConnection.SendAsync("Movement", keyData.ToString());
                    return true;
                }
                else if (keyData == Keys.Space)
                {
                    //put a bomb
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:44344/hub/").Build();
            hubConnection.On<int[][]>("StateUpdate", message => world = message);
            await hubConnection.StartAsync();
            button1.Visible = false;
        }
    }
}
