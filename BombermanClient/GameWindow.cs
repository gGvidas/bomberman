using BombermanClasses;
using BombermanClasses.Walls;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class   GameWindow : Form
    {
        bool IsGameOver = true;

        Int32 squareSize = 20;
        Int32 numSquaresX = 32;
        Int32 numSquaresY = 32;

        private Tile[][] world;

        private HubConnection hubConnection;

        Image img = null;
        Graphics imgGraph = null;
        Graphics graph = null;

        public GameWindow()
        {
            InitializeComponent();
            buttonStart.Enabled = false;
            InitConnection();
            this.screen.Visible = false;

            img = new Bitmap(squareSize * numSquaresX, squareSize * numSquaresY);
            imgGraph = Graphics.FromImage(img);
            graph = screen.CreateGraphics();

        }

    
        public void StateUpdated(string serializedWordFromServer)
        {
            var worldFromServer = JsonConvert.DeserializeObject<Tile[][]>(serializedWordFromServer, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            world = worldFromServer;
            Draw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textGameOver.Visible = false;

            this.timer1.Interval = 200;
            timer1.Start();
        }

        private void ChangeGameState()
        {
            IsGameOver = !IsGameOver;

            this.screen.Visible = !IsGameOver;
            this.textGameOver.Visible = IsGameOver;
            this.buttonStart.Visible = IsGameOver;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            ChangeGameState();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void Draw()
        {
            imgGraph.FillRectangle(new SolidBrush(Color.White), 0, 0, squareSize * numSquaresX, squareSize * numSquaresY);

            var gridBrush = new SolidBrush(Color.LightGray);
            var gridPen = new Pen(gridBrush);

            for (int i = 1; i < numSquaresX; ++i)
                imgGraph.DrawLine(gridPen, 0, i * squareSize, squareSize * numSquaresX, i * squareSize);

            for (int i = 1; i < numSquaresX; ++i)
                imgGraph.DrawLine(gridPen, i * squareSize, 0, i * squareSize, squareSize * numSquaresY);

            if (world == null) return;

            //Draw
            var playerColor = new SolidBrush(Color.Green);
            var pathColor = new SolidBrush(Color.SandyBrown);
            var rockColor = new SolidBrush(Color.RosyBrown);
            var wallColor = new SolidBrush(Color.Brown);

            for (int i = 0; i < world.GetLength(0); i++)
            {
                for (int j = 0; j < world[i].Length; j++)
                {
                    if (world[i][j].player != null)
                        imgGraph.FillRectangle(playerColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1);
                    else if (world[i][j].mapObject is DestructableWall)
                        world[i][j].mapObject.Draw(rockColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                    else if (world[i][j].mapObject is IndestructableWall)
                        world[i][j].mapObject.Draw(wallColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                    else 
                        imgGraph.FillRectangle(pathColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1);
                }
            }

            graph.DrawImage(img, 0, 0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
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

        private async Task InitConnection()
        {
            hubConnection = new HubConnectionBuilder()
                            .WithUrl("https://localhost:5001/hub/")
                            .Build();
            hubConnection.On<string>("StateUpdate", StateUpdated);

            await hubConnection.StartAsync();
            buttonStart.Enabled = true;
        }

    }
}
