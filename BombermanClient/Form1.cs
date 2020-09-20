using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace BombermanClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            var hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:44344/hub/").Build();
            hubConnection.On<string>("StateUpdate", message => Label1.Text = message);
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("UpdateClients");
        }
    }
}
