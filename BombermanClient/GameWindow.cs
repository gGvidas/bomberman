using BombermanClasses;
using BombermanClasses.BombNameSpace;
using BombermanClasses.Items;
using BombermanClasses.Proxy;
using BombermanClasses.Walls;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class   GameWindow : Form
    {
        bool IsGameOver = true;

        Int32 squareSize = 20;
        Int32 numSquaresX;
        Int32 numSquaresY;

        private Tile[][] world;

        private HubConnection hubConnection;

        Image img = null;
        Graphics imgGraph = null;
        Graphics graph = null;

        [NonSerialized]        
        private Image Background_;

        private static string Id;
        private static string PlayerName;

        public GameWindow()
        {
            InitializeComponent();
            buttonStart.Enabled = false;
            InitConnection();
            this.screen.Visible = false;

            graph = screen.CreateGraphics();

        }

    
        public void StateUpdated(string serializedWordFromServer)
        {
            var state = JsonConvert.DeserializeObject<StateDTO>(serializedWordFromServer, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            numSquaresX = state.Objects.GetLength(0);
            numSquaresY = state.Objects[0].Length;

            if (state.AlivePlayersIds.Count == 1 && 
                state.DeadPlayersIds.Count > 0 && 
                state.AlivePlayersIds.Any(alivePlayer => alivePlayer == Id))
            {
                this.winnerLabel.Visible = true;
                this.buttonRestart.Visible = true;
            }
            else if (state.DeadPlayersIds.Any(deadPlayer => deadPlayer == Id))
            {
                this.textGameOver.Visible = true;
                if(state.AlivePlayersIds.Count == 1)
                    this.buttonRestart.Visible = true;
            }
            else
            {
                this.textGameOver.Visible = false;
                this.buttonRestart.Visible = false;
                this.winnerLabel.Visible = false;
            }

            //this.score.Text = $"Score: {state.PlayerScores.Where(playerScore => playerScore.Id == Id).Select(playerScore => playerScore.score).First().ToString()}";
            var proxy = new LeaderboardProxy { Id = Id, _realLeaderboard = state.PlayerScores };
            score.Text = BuildLeaderBoard(proxy.GetLeaderBoard());

            img = new Bitmap(squareSize * numSquaresX, squareSize * numSquaresY);
            imgGraph = Graphics.FromImage(img);

            world = state.Objects;
            Draw();
         
        }

        private static string BuildLeaderBoard(List<LeaderBoardRow> rows)
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Format("High scores:"));

            builder.AppendLine(string.Format("{0, 30}|{1, 30}|{2, 30}", "Rank", "Name", "Score"));
            builder.AppendLine(new string('-', 80));

            foreach (var row in rows)
            {
                var name = row.Id == Id ? string.IsNullOrEmpty(PlayerName) ? row.Name : PlayerName : row.Name;

                builder.AppendLine(string.Format("{0, 30}|{1, 30}|{2, 30}", row.Rank, name, row.score));
            }

            return builder.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.textGameOver.Visible = false;
            this.winnerLabel.Visible = false;
            this.buttonRestart.Visible = false;

            this.timer1.Interval = 200;
            timer1.Start();
        }

        private void ChangeGameState()
        {
            this.textBox1.Visible = false;
            PlayerName = this.textBox1.Text;
            IsGameOver = !IsGameOver;

            this.screen.Visible = !IsGameOver;
            this.buttonStart.Visible = IsGameOver;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            ChangeGameState();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            hubConnection.SendAsync("UpdateClients");
        }
        public Image Background
        {
            get
            {
                return Background_;
            }

            set
            {
                Background_ = value;
            }
        }

        private void Draw()
        {
            var spritesFolder = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..");
            this.Background = Image.FromFile(Path.Combine(spritesFolder, @"Sprites\World.jpg"));
            if (Background != null)
            {
                imgGraph.DrawImage(Background, 0, 0, squareSize * numSquaresX, squareSize * numSquaresY);
            }
           
            var gridBrush = new SolidBrush(Color.LightGray);
            var gridPen = new Pen(gridBrush);

            for (int i = 1; i < numSquaresX; ++i)
                imgGraph.DrawLine(gridPen, 0, i * squareSize, squareSize * numSquaresX, i * squareSize);

            for (int i = 1; i < numSquaresX; ++i)
                imgGraph.DrawLine(gridPen, i * squareSize, 0, i * squareSize, squareSize * numSquaresY);

            if (world == null) return;

            //Draw
            Image playerColor = Image.FromFile(Path.Combine(spritesFolder, @"Sprites\Player.png"));
            Image rockColor = Image.FromFile(Path.Combine(spritesFolder, @"Sprites\BlockDestructible.png"));
            Image wallColor = Image.FromFile(Path.Combine(spritesFolder, @"Sprites\BlockNonDestructible.png"));
            Image bombColor = Image.FromFile(Path.Combine(spritesFolder, @"Sprites\Bombe.png"));
            Image firebombColor = Image.FromFile(Path.Combine(spritesFolder, @"Sprites\Fire_Bomb.png"));
            Image icebombColor = Image.FromFile(Path.Combine(spritesFolder, @"Sprites\Ice_Bomb.png"));
            Image fireColor = Image.FromFile(Path.Combine(spritesFolder, @"Sprites\Fire.png"));
            Image iceColor = Image.FromFile(Path.Combine(spritesFolder, @"Sprites\BlockIce.png"));

            Image itemColor = Image.FromFile(Path.Combine(spritesFolder, @"Sprites\BlockItem.png"));
            Image fireshieldColor = Image.FromFile(Path.Combine(spritesFolder, @"Sprites\Fire_Shield.png"));
            Image iceshieldColor = Image.FromFile(Path.Combine(spritesFolder, @"Sprites\Ice_Shield.png"));

            for (int i = 0; i < world.GetLength(0); i++)
            {
                for (int j = 0; j < world[i].Length; j++)
                {

                    if (world[i][j].entity is Player)
                    {
                        if (world[i][j].onfiretype != null)
                            world[i][j].onfiretype.Draw(playerColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                        else if (world[i][j].firetype != null)
                            world[i][j].firetype.Draw(playerColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                        else if (world[i][j].icetype != null)
                            world[i][j].icetype.Draw(playerColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                        else
                            world[i][j].entity.Draw(playerColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                    }
                    else if (world[i][j].bomb is FireBomb)
                        world[i][j].bomb.Draw(firebombColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                    else if (world[i][j].bomb is IceBomb)
                        world[i][j].bomb.Draw(icebombColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                    else if (world[i][j].bomb != null)
                        world[i][j].bomb.Draw(bombColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);

                    else if (world[i][j].entity is Fire)
                        imgGraph.DrawImage(fireColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1);
                    else if (world[i][j].entity is IceWall)
                        imgGraph.DrawImage(iceColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1);
                    else if (world[i][j].entity is DestructableWall)
                        world[i][j].entity.Draw(rockColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                    else if (world[i][j].entity is ItemDropWall)
                        world[i][j].entity.Draw(itemColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                    else if (world[i][j].entity is IndestructableWall)
                        world[i][j].entity.Draw(wallColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);

                    else if (world[i][j].item is FireBomb)
                        world[i][j].item.Draw(firebombColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                    else if (world[i][j].item is IceBomb)
                        world[i][j].item.Draw(icebombColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                    else if (world[i][j].item is FireShield)
                        world[i][j].item.Draw(fireshieldColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);
                    else if (world[i][j].item is IceShield)
                        world[i][j].item.Draw(iceshieldColor, i * squareSize, j * squareSize, squareSize - 1, squareSize - 1, imgGraph);

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
                if (keyData == Keys.A || keyData == Keys.S || keyData == Keys.D || keyData == Keys.W || 
                    keyData == Keys.F || keyData == Keys.I || keyData == Keys.O || keyData == Keys.M)
                {
                    hubConnection.SendAsync("Movement", keyData.ToString());
                    return true;
                }
                else if (keyData == Keys.Space)
                {
                    hubConnection.SendAsync("PutDownBomb");
                    Task.Factory.StartNew(() => Thread.Sleep(4000))
                    .ContinueWith((t) =>
                    {
                        hubConnection.SendAsync("StateUpdate");

                    }, TaskScheduler.FromCurrentSynchronizationContext());
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async Task InitConnection()
        {

            hubConnection = new HubConnectionBuilder()
                            .WithUrl("http://localhost:5000/hub")
                            .Build();
            hubConnection.On<string>("StateUpdate", StateUpdated);
            try
            {
                await hubConnection.StartAsync();
                Id = hubConnection.ConnectionId;
                buttonStart.Enabled = true;
            }catch(Exception e)
            {

            }
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            this.textGameOver.Visible = false;
            this.winnerLabel.Visible = false;
            this.buttonRestart.Visible = false;

            if (hubConnection != null)
            {
                hubConnection.SendAsync("RestartGame");
            }
        }

        private void textGameOver_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
