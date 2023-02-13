using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Privatizare
{
    public partial class Form1 : Form
    {
        bool ending = false;
        Random r = new Random();
        int banca, currloto = 0, currtelex = 0;
        int[] loto = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 13, 13, 13, 13 };
        int[] telex = new int[] { 0, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3, 4, 5, 6, 6, 7, 8, 8, 8, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
    class Space
        {

            public int type;
            ///1
            public string propname;
            public int proprice;
            public int rent;

            public int currupgrade;
            public int upgrademax;
            public int[] upgradeprice;
            public int[] upgraderent;

            public int owner;

            public Space next;
            public Space prev;
            public Space(int type, string propname, int proprice, int rent, int upgrademax)
            {
                this.type = type;
                this.propname = propname;
                this.proprice = proprice;
                this.rent = rent;
                currupgrade = 0;
                this.upgrademax = upgrademax;
                owner = 0;
               

            }
            public Space(int type, string propname)
            {
                this.type = type;
                this.propname = propname;
            }



        }
        
        class Player
        {
           public int money;
           public int exhaust;
           public Space currspace;

           public Player(Space s)
            {
                money = 85500;
                exhaust = 0;
                currspace = s;
            }
        }
        

        void randomizearray(int []arr, int s)
        {
            
            for (int i = 0; i < s; i++)
            {
                int a = (r.Next(s));
                //swap(arr[i], arr[a]);
                int aux;
                aux = arr[i];
                arr[i] = arr[a];
                arr[a] = aux;
            }


        }

        void end()
        {
            int winner, winnerbal;
            if (players[1].money > players[2].money && players[1].money > players[3].money)
            {
                winner = 1;
                winnerbal = players[1].money;
            }
            else
                if (players[2].money > players[1].money && players[2].money > players[3].money)
            {
                winner = 2;
                winnerbal = players[2].money;
            }
            else
            {
                winner = 3;
                winnerbal = players[3].money;
            }


            if (banca < 0)
            {
                //bancafaliment++;
                richTextBox1.Text +=Environment.NewLine + "The bank went bankrupt. Player " + winner.ToString() + " wins with " + winnerbal.ToString() + " ECU";
                button1.Hide();
                button7.Hide();
            }
            if (players[1].money < 0)
            {
                //p1faliment++;
                richTextBox1.Text += Environment.NewLine + "Player 1 went bankrupt. Player " + winner.ToString() + " wins with " + winnerbal.ToString() + " ECU";
                button1.Hide();
                button7.Hide();
            }
            if (players[2].money < 0)
            {
                //p2faliment++;
                richTextBox1.Text += Environment.NewLine + "Player 2 went bankrupt. Player " + winner.ToString() + " wins with " + winnerbal.ToString() + " ECU";
                button1.Hide();
                button7.Hide();
            }
            if (players[3].money < 0)
            {
                //p3faliment++;
                richTextBox1.Text += Environment.NewLine + "Player 3 went bankrupt. Player " + winner.ToString() + " wins with " + winnerbal.ToString() + " ECU";
                button1.Hide();
                button7.Hide();
            }
        }

        void onprop(int p, ref Space s)
        {
            if (s.owner == 0 && players[p].money >= s.proprice)
            {
                if (p == 1)
                {
                    label47.Text = "You have landed on " + s.propname + ". Do you want to buy it?";
                    label47.Show();
                    button3.Show();
                    button4.Show();
                }
                else
                {
                    s.owner = p;
                    players[p].money -= s.proprice;
                    banca += s.proprice;
                    richTextBox1.Text += Environment.NewLine + "Player" + p + " purchased " + s.propname + ". Rent is" + s.rent.ToString();
                }
                //g << "player " << p << " purchased " << s.propname << " rent is " << s.rent << endl;

            }
            else
            {
                if (s.owner == p)
                {
                    if (s.currupgrade < s.upgrademax && players[p].money >= s.upgradeprice[s.currupgrade + 1])
                    {
                        if (p == 1)
                        {
                            label47.Text = "You have landed on " + s.propname + ". Do you want to upgrade it?";
                            label47.Show();
                            button5.Show();
                            button6.Show();

                        }
                        else
                        {


                            s.currupgrade++;
                            players[p].money -= s.upgradeprice[s.currupgrade];
                            banca += s.upgradeprice[s.currupgrade];
                            s.rent += s.upgraderent[s.currupgrade];
                            richTextBox1.Text += Environment.NewLine + "Player" + p + " upgraded " + s.propname + ". Rent is" + s.rent.ToString();
                        }
                        //g << "player " << p << " upgraded " << s.propname << " rent is " << s.rent << endl;
                    }
                }
                else
                {
                    players[s.owner].money += s.rent;
                    players[p].money -= s.rent;
                    richTextBox1.Text += Environment.NewLine + "Player" + p + " landed on " + s.propname + " and paid player " +s.owner.ToString() +" " + s.rent.ToString();
                    // g << "player " << p << " landed on " << s.propname << " and paid player " << s.owner << " " << s.rent << endl;

                }


            }



        }

        void onloto(int p)
        {

            richTextBox1.Text += Environment.NewLine + "Player" + p.ToString() + " landed on LOTO and ";
            //g << "player " << p << " landed on LOTO and ";
            switch (loto[currloto])
            {
                case 0:
                    players[p].money += 500;
                    banca -= 500;
                    richTextBox1.Text += "won 500.";
                    //g << "won 500" << endl;
                    break;
                case 1:
                    players[p].money += 1000;
                    banca -= 1000;
                    richTextBox1.Text += "won 1000.";
                    //g << "won 1000" << endl;
                    break;
                case 2:
                    players[p].money += 2000;
                    banca -= 2000;
                    richTextBox1.Text += "won 2000.";
                    //g << "won 2000" << endl;
                    break;
                case 3:
                    players[p].money += 3000;
                    banca -= 3000;
                    richTextBox1.Text += "won 3000.";
                    //g << "won 3000" << endl;
                    break;
                case 4:
                    players[p].money += 4000;
                    banca -= 4000;
                    richTextBox1.Text += "won 4000.";
                    //g << "won 4000" << endl;
                    break;
                case 5:
                    players[p].money += 10000;
                    banca -= 10000;
                    richTextBox1.Text += "won 10000.";
                    //g << "won 10000" << endl;
                    break;
                case 6:
                    players[p].money += 6000;
                    banca -= 6000;
                    richTextBox1.Text += "won 6000.";
                    //g << "won 6000" << endl;
                    break;
                case 7:
                    players[p].money += 5000;
                    banca -= 5000;
                    richTextBox1.Text += "won 5000.";
                    //g << "won 5000" << endl;
                    break;
                case 8:
                    players[p].money -= 5000;
                    banca += 5000;
                    richTextBox1.Text += "lost 5000.";
                    //g << "lost 5000" << endl;
                    break;
                case 9:
                    players[p].money -= 4000;
                    banca += 4000;
                    richTextBox1.Text += "lost 4000.";
                    //g << "lost 4000" << endl;
                    break;
                case 10:
                    players[p].money -= 3000;
                    banca += 3000;
                    richTextBox1.Text += "lost 3000.";
                    //g << "lost 3000" << endl;
                    break;
                case 11:
                    players[p].money -= 2000;
                    banca += 2000;
                    richTextBox1.Text += "lost 2000.";
                    //g << "lost 2000" << endl;
                    break;
                case 12:
                    players[p].money -= 1000;
                    banca += 1000;
                    richTextBox1.Text += "lost 1000.";
                    //g << "lost 1000" << endl;
                    break;
                default:
                    richTextBox1.Text += "nothing happened.";
                    //g << "nothing happened" << endl;
                    break;


            }
            currloto++;
            if (currloto == 18)
            {
                currloto = 0;
                randomizearray(loto, 18);
            }


        }

        void onconsfatuiri(int p)
        {
            players[p].money -= 1000;
            banca += 1000;
            richTextBox1.Text += Environment.NewLine + "Player" + p.ToString() + " landed on Conference Room and lost 1000.'";
            //g << "player " << p << " landed on Conference Room and lost 1000" << endl;
        }

        void onmergilastart(int p)
        {
            players[p].currspace = s1;
            richTextBox1.Text += Environment.NewLine + "Player" + p.ToString() + " landed on Go to Start.";
            //g << "player " << p << " landed on Go To Start" << endl;
        }

        void onodihna(int p)
        {
            players[p].exhaust = 2;
            richTextBox1.Text += Environment.NewLine + "Player" + p.ToString() + " landed on Rest.";
            //g << "player " << p << " landed on Rest" << endl;
        }

        void ondividente(int p)
        {
            players[p].money += 2000;
            banca -= 2000;
            richTextBox1.Text += Environment.NewLine + "Player" + p.ToString() + " landed on Dividents and won 2000.";
            //g << "player " << p << " landed on Dividends and won 2000" << endl;

        }

        void onbanca(int p)
        {
            players[p].money += 5000;
            banca -= 5000;
            richTextBox1.Text += Environment.NewLine + "Player" + p.ToString() + " landed on Bank and won 5000 .";
            //g << "player " << p << " landed on Bank and won 5000" << endl;
        }

        void ontelex(int p)
        {
            richTextBox1.Text += Environment.NewLine + "Player" + p.ToString() + " landed on TELEX and ";
            //g << "player " << p << " landed on TELEX and ";
            switch (telex[currtelex])
            {
                case 0:
                    players[p].money += 6000;
                    for (int i = 1; i < 4; i++)
                        players[p].money -= 2000;
                    richTextBox1.Text += "won 2000 from each player.";
                    //g << "won 2000 from each player" << endl;
                    break;
                case 1:
                    players[p].money -= 3000;
                    banca += 3000;
                    richTextBox1.Text += "lost 3000.";
                    //g << "lost 3000" << endl;
                    break;
                case 2:
                    //platesti helicon 3000
                    if (s7.owner == 0)
                    {
                        players[p].money -= 3000;
                        banca += 3000;
                    }
                    else
                        if (s7.owner != p)
                    {
                        players[p].money -= 3000;
                        players[s7.owner].money += 3000;
                    }
                    richTextBox1.Text += "paid the owner of Helicon 3000.";
                    //g << "paid Helicon 3000" << endl;

                    break;
                case 3:
                    players[p].money -= 2000;
                    banca += 2000;
                    richTextBox1.Text += "lost 2000.";
                    //g << "lost 2000" << endl;
                    break;
                case 4:
                    players[p].money -= 2000;
                    banca += 2000;
                    richTextBox1.Text += "lost 2000.";
                    //g << "lost 2000" << endl;
                    break;
                case 5:
                    players[p].money += 4000;
                    banca -= 4000;
                    richTextBox1.Text += "lost 4000.";
                    //g << "won 4000" << endl;
                    break;
                case 6:
                    players[p].money += 10000;
                    banca -= 10000;
                    richTextBox1.Text += "won 10000.";
                    //g << "won 10000" << endl;
                    break;
                case 7:
                    richTextBox1.Text += "went back 4 spaces.";
                    //g << "went back 4 spaces" << endl;
                    for (int i = 0; i < 4; i++)
                        players[p].currspace = players[p].currspace.prev;
                    switch (players[p].currspace.type)
                    {
                        case 1:
                            onprop(p, ref players[p].currspace);
                            break;
                        case 2:
                            onloto(p);
                            break;
                        case 3:
                            ontelex(p);
                            break;
                        case 4:
                            onconsfatuiri(p);
                            break;
                        case 5:
                            onbanca(p);
                            break;
                        case 6:
                            onmergilastart(p);
                            break;
                        case 7:
                            onodihna(p);
                            break;
                        case 8:
                            ondividente(p);
                            break;
                        default:
                            break;



                    }

                    break;
                case 8:
                    players[p].money -= 4000;
                    banca += 4000;
                    richTextBox1.Text += "lost 4000.";
                    //g << "lost 4000" << endl;
                    break;
                case 9:
                    if (s11.owner == 0)
                    {
                        players[p].money -= 5000;
                        banca += 5000;
                    }
                    else
                        if (s11.owner != p)
                    {
                        players[p].money -= 5000;
                        players[s11.owner].money += 5000;
                    }
                    richTextBox1.Text += "paid the owner of Timisoreana 4000.";
                    //g << "paid Timisoara 5000" << endl;
                    break;
                case 10:
                    if (s34.owner == 0)
                    {
                        players[p].money -= 5000;
                        banca += 5000;
                    }
                    else
                        if (s34.owner != p)
                    {
                        players[p].money -= 5000;
                        players[s34.owner].money += 5000;
                    }
                    richTextBox1.Text += "paid the owner of Televizoare 4000.";
                    //g << "paid TV 5000" << endl;
                    break;
                case 11:
                    if (s35.owner == 0)
                    {
                        players[p].money -= 6000;
                        banca += 6000;
                    }
                    else
                        if (s35.owner != p)
                    {
                        players[p].money -= 6000;
                        players[s35.owner].money += 6000;
                    }
                    richTextBox1.Text += "paid the owner of Poli 4000.";
                    //g << "payed Poli 6000" << endl;
                    break;
                case 12:
                    richTextBox1.Text += "advanced 4 spaces.";
                    //g << "went forward 4 spaces" << endl;
                    for (int i = 0; i < 4; i++)
                        players[p].currspace = players[p].currspace.next;
                    switch (players[p].currspace.type)
                    {
                        case 1:
                            onprop(p, ref players[p].currspace);
                            break;
                        case 2:
                            onloto(p);
                            break;
                        case 3:
                            ontelex(p);
                            break;
                        case 4:
                            onconsfatuiri(p);
                            break;
                        case 5:
                            onbanca(p);
                            break;
                        case 6:
                            onmergilastart(p);
                            break;
                        case 7:
                            onodihna(p);
                            break;
                        case 8:
                            ondividente(p);
                            break;
                        default:
                            break;



                    }

                    break;
                case 13:
                    if (s33.owner == 0)
                    {
                        players[p].money -= 3000;
                        banca += 3000;
                    }
                    else
                        if (s33.owner != p)
                    {
                        players[p].money -= 3000;
                        players[s33.owner].money += 3000;
                    }
                    richTextBox1.Text += "paid the owner of Aviatica 3000.";
                    //g << "payed Aviatica 3000" << endl;
                    break;
                case 14:
                    players[p].money -= 6000;
                    banca += 6000;
                    richTextBox1.Text += "lost 6000.";
                    //g << "lost 6000" << endl;
                    break;
                case 15:
                    if (s6.owner == 0)
                    {
                        players[p].money -= 3000;
                        banca += 3000;
                    }
                    else
                        if (s6.owner != p)
                    {
                        players[p].money -= 3000;
                        players[s6.owner].money += 3000;
                    }
                    richTextBox1.Text += "paid the owner of Telefoane 3000.";
                    //g << "payed Telefoane 3000" << endl;
                    break;
                case 16:
                    players[p].money += 5000;
                    banca -= 5000;
                    richTextBox1.Text += "won 5000.";
                    //g << "won 5000" << endl;
                    break;
                case 17:
                    players[p].money -= 5000;
                    banca += 5000;
                    richTextBox1.Text += "lost 5000.";
                    //g << "lost 5000" << endl;
                    break;
                case 18:
                    players[p].money -= 3000;
                    banca += 3000;
                    players[p].exhaust = 2;
                    richTextBox1.Text += "is resting for 2 turns, as well as losing 3000.";
                    //g << "is resting for 2 turns, as well as losing 3000" << endl;
                    break;


            }
            currtelex++;
            if (currtelex == 30)
            {
                currtelex = 0;
                randomizearray(telex, 30);
            }


        }



        Space s1 = new Space(9, "Start");
        Space s2 = new Space(1, "Continental", 5000, 500, 3);
        Space s3 = new Space(2, "LOTO");
        Space s4 = new Space(1, "Dura", 2500, 350, 3);
        Space s5 = new Space(1, "CFR", 2000, 200, 3);
        Space s6 = new Space(1, "Telefoane", 3000, 400, 2);
        Space s7 = new Space(1, "Helicon", 3750, 400, 3);
        Space s8 = new Space(1, "Lahovary", 3000, 350, 2);
        Space s9 = new Space(3, "Telex");
        Space s10 = new Space(1, "Unirii", 1000, 450, 2);
        Space s11 = new Space(1, "Timisoreana", 2000, 250, 3);
        Space s12 = new Space(4, "Consfatuiri");
        Space s13 = new Space(1, "Ambasador", 3000, 350, 3);
        Space s14 = new Space(1, "Taberei", 3100, 350, 3);
        Space s15 = new Space(1, "Lipscani",3100,450,2);
        Space s16 = new Space(5, "Banca");
        Space s17 = new Space(1, "Maria",2500,450,3);
        Space s18 = new Space(1, "Victoriei",4000,400,2);
        Space s19 = new Space(6, "Return");
        Space s20 = new Space(1, "Electromotor",1000,550,3);
        Space s21 = new Space(1, "Romana",4000,450,3);
        Space s22 = new Space(2, "LOTO");
        Space s23 = new Space(1, "Guban",1000,300,3);
        Space s24 = new Space(1, "Metalotim",4000,800,2);
        Space s25 = new Space(1, "Celuloza",4000,500,2);
        Space s26 = new Space(1, "Prichindel",3000,400,2);
        Space s27 = new Space(3, "TELEX");
        Space s28 = new Space(1, "Padure",1000,250,3);
        Space s29 = new Space(1, "Memorii",2000,500,3);
        Space s30 = new Space(1, "Sagului",3500,450,2);
        Space s31 = new Space(7, "Odihna");
        Space s32 = new Space(1, "Circumvalatiuni",4000,600,2);
        Space s33 = new Space(1, "Aviatica",1000,400,3);
        Space s34 = new Space(1, "Televiziune",2000,450,3);
        Space s35 = new Space(1, "Poli",1000,200,3);
        Space s36 = new Space(3, "TELEX");
        Space s37 = new Space(1, "OHM",2100,200,3);
        Space s38 = new Space(8, "Dividente");

        Player[] players = new Player[4];

        void init()
        {


            ///CONTINENTAL
            s2.upgradeprice = new int[4];
            s2.upgraderent = new int[4];
            s2.upgradeprice[1] = 4000;
            s2.upgradeprice[2] = 7000;
            s2.upgradeprice[3] = 12000;
            s2.upgraderent[1] = 1400;
            s2.upgraderent[2] = 2000;
            s2.upgraderent[3] = 3000;

            ///DURA 
            s4.upgradeprice = new int[4];
            s4.upgraderent = new int[4];
            s4.upgradeprice[1] = 2000;
            s4.upgradeprice[2] = 3200;
            s4.upgradeprice[3] = 5000;
            s4.upgraderent[1] = 600;
            s4.upgraderent[2] = 850;
            s4.upgraderent[3] = 1450;

            ///CFR
            s5.upgradeprice = new int[4];
            s5.upgraderent = new int[4];
            s5.upgradeprice[1] = 3000;
            s5.upgradeprice[2] = 4000;
            s5.upgradeprice[3] = 5500;
            s5.upgraderent[1] = 400;
            s5.upgraderent[2] = 800;
            s5.upgraderent[3] = 1550;

            ///TELEFOANE
            s6.upgradeprice = new int[4];
            s6.upgraderent = new int[4];
            s6.upgradeprice[1]=2100;
            s6.upgradeprice[2] = 3250;
            s6.upgraderent[1] = 800;
            s6.upgraderent[2] = 1450;

            ///HELICON
            s7.upgradeprice = new int[4];
            s7.upgraderent = new int[4];
            s7.upgradeprice[1] = 2000;
            s7.upgradeprice[2] = 3250;
            s7.upgradeprice[3] = 5850;
            s7.upgraderent[1] = 500;
            s7.upgraderent[2] = 1200;
            s7.upgraderent[3] = 1300;

            ///LAHOVARY
            s8.upgradeprice = new int[4];
            s8.upgraderent = new int[4];
            s8.upgradeprice[1]= 4100;
            s8.upgradeprice[2] = 7250;
            s8.upgraderent[1] = 750;
            s8.upgraderent[2] = 1350;

            ///UNIRII
            s10.upgradeprice = new int[4];
            s10.upgraderent = new int[4];
            s10.upgradeprice[1] = 4000;
            s10.upgradeprice[2] = 6000;
            s10.upgraderent[1] = 1050;
            s10.upgraderent[2] = 1550;

            ///TIMISOREANA
            s11.upgradeprice = new int[4];
            s11.upgraderent = new int[4];
            s11.upgradeprice[1] = 4000;
            s11.upgradeprice[2] = 7000;
            s11.upgradeprice[3] = 11000;
            s11.upgraderent[1] = 600;
            s11.upgraderent[2] = 1400;
            s11.upgraderent[3] = 2400;

            ///AMBASADOR
            s13.upgradeprice = new int[4];
            s13.upgraderent = new int[4];
            s13.upgradeprice[1] = 4000;
            s13.upgradeprice[2] = 6000;
            s13.upgradeprice[3] = 12000;
            s13.upgraderent[1] = 500;
            s13.upgraderent[2] = 1500;
            s13.upgraderent[3] = 2350;

            ///TABEREI
            s14.upgradeprice = new int[4];
            s14.upgraderent = new int[4];
            s14.upgradeprice[1] = 1550;
            s14.upgradeprice[2] = 4000;
            s14.upgradeprice[3] = 7150;
            s14.upgraderent[1] = 350;
            s14.upgraderent[2] = 850;
            s14.upgraderent[3] = 1400;

            ///LIPSCANI
            s15.upgradeprice = new int[4];
            s15.upgraderent = new int[4];
            s15.upgradeprice[1]=4200;
            s15.upgradeprice[2] = 6950;
            s15.upgraderent[1] = 800;
            s15.upgraderent[2] = 1350;

            ///MARIA
            s17.upgradeprice = new int[4];
            s17.upgraderent = new int[4];
            s17.upgradeprice[1] = 3200;
            s17.upgradeprice[2] = 5100;
            s17.upgradeprice[3] = 6750;
            s17.upgraderent[1] = 750;
            s17.upgraderent[2] = 1050;
            s17.upgraderent[3] = 1350;

            ///VICTORIEI
            s18.upgradeprice = new int[4];
            s18.upgraderent = new int[4];
            s18.upgradeprice[1] = 6000;
            s18.upgradeprice[2] = 11000;
            s18.upgraderent[1] = 1000;
            s18.upgraderent[2] = 2300;

            ///ELECTROMOTOR
            s20.upgradeprice = new int[4];
            s20.upgraderent = new int[4];
            s20.upgradeprice[1] = 2550;
            s20.upgradeprice[2] = 5100;
            s20.upgradeprice[3] = 11000;
            s20.upgraderent[1] = 350;
            s20.upgraderent[2] = 1100;
            s20.upgraderent[3] = 2350;

            ///ROMANA
            s21.upgradeprice = new int[4];
            s21.upgraderent = new int[4];
            s21.upgradeprice[1] = 2650;
            s21.upgradeprice[2] = 5200;
            s21.upgradeprice[3] = 7700;
            s21.upgraderent[1] = 650;
            s21.upgraderent[2] = 1100;
            s21.upgraderent[3] = 1550;

            ///GUBAN
            s23.upgradeprice = new int[4];
            s23.upgraderent = new int[4];
            s23.upgradeprice[1]= 1500;
            s23.upgradeprice[2] = 3000;
            s23.upgradeprice[3] = 4100;
            s23.upgraderent[1] = 750;
            s23.upgraderent[2] = 950;
            s23.upgraderent[3] = 1150;

            ///METALOTIM
            s24.upgradeprice = new int[4];
            s24.upgraderent = new int[4];
            s24.upgradeprice[1] = 3000;
            s24.upgradeprice[2] = 4000;
            s24.upgraderent[1] = 800;
            s24.upgraderent[2] = 1100;

            ///CELULOZA
            s25.upgradeprice = new int[4];
            s25.upgraderent = new int[4];
            s25.upgradeprice[1]=4000;
            s25.upgradeprice[2] = 4500;
            s25.upgraderent[1] = 800;
            s25.upgraderent[2] = 1000;

            ///PRICHINDEL
            s26.upgradeprice = new int[4];
            s26.upgraderent = new int[4];
            s26.upgradeprice[1] = 3000;
            s26.upgradeprice[2] = 3500;
            s26.upgraderent[1] = 550;
            s26.upgraderent[2] = 850;

            ///PADURE
            s28.upgradeprice = new int[4];
            s28.upgraderent = new int[4];
            s28.upgradeprice[1] = 1500;
            s28.upgradeprice[2] = 2000;
            s28.upgradeprice[3] = 4000;
            s28.upgraderent[1] = 250;
            s28.upgraderent[2] = 400;
            s28.upgraderent[3] = 850;

            ///MEMORII
            s29.upgradeprice = new int[4];
            s29.upgraderent = new int[4];
            s29.upgradeprice[1] = 2500;
            s29.upgradeprice[2] = 3000;
            s29.upgradeprice[3] = 4000;
            s29.upgraderent[1] = 550;
            s29.upgraderent[2] = 900;
            s29.upgraderent[3] = 1150;

            ///SAGULUI
            s30.upgradeprice = new int[4];
            s30.upgraderent = new int[4];
            s30.upgradeprice[1] = 5000;
            s30.upgradeprice[2] = 8000;
            s30.upgraderent[1] = 1300;
            s30.upgraderent[2] = 1650;

            ///CIRCUMVALATIUNI
            s32.upgradeprice = new int[4];
            s32.upgraderent = new int[4];
            s32.upgradeprice[1] = 4500;
            s32.upgradeprice[2] = 9500;
            s32.upgraderent[1] = 1050;
            s32.upgraderent[2] = 2250;

            ///AVIATICA
            s33.upgradeprice = new int[4];
            s33.upgraderent = new int[4];
            s33.upgradeprice[1]=2500;
            s33.upgradeprice[2] = 3500;
            s33.upgradeprice[3] = 4500;
            s33.upgraderent[1] = 550;
            s33.upgraderent[2] = 750;
            s33.upgraderent[3] = 1050;

            ///TELEVIZIUNE
            s34.upgradeprice = new int[4];
            s34.upgraderent = new int[4];
            s34.upgradeprice[1] = 1500;
            s34.upgradeprice[2] = 2000;
            s34.upgradeprice[3] = 6000;
            s34.upgraderent[1] = 450;
            s34.upgraderent[2] = 600;
            s34.upgraderent[3] = 1150;

            ///POLI
            s35.upgradeprice = new int[4];
            s35.upgraderent = new int[4];
            s35.upgradeprice[1]=2200;
            s35.upgradeprice[2] = 3000;
            s35.upgradeprice[3] = 5500;
            s35.upgraderent[1] = 350;
            s35.upgraderent[2] = 800;
            s35.upgraderent[3] = 1250;

            ///OHM
            s37.upgradeprice = new int[4];
            s37.upgraderent = new int[4];
            s37.upgradeprice[1] = 2200;
            s37.upgradeprice[2] = 2950;
            s37.upgradeprice[3] = 5700;
            s37.upgraderent[1] = 350;
            s37.upgraderent[2] = 850;
            s37.upgraderent[3] = 1350;

            ///MOVEMENT
            s1.next = s2;
            s2.next = s3;
            s3.next = s4;
            s4.next = s5;
            s5.next = s6;
            s6.next = s7;
            s7.next = s8;
            s8.next = s9;
            s9.next = s10;
            s10.next = s11;
            s11.next = s12;
            s12.next = s13;
            s13.next = s14;
            s14.next = s15;
            s15.next = s16;
            s16.next = s17;
            s17.next = s18;
            s18.next = s19;
            s19.next = s20;
            s20.next = s21;
            s21.next = s22;
            s22.next = s23;
            s23.next = s24;
            s24.next = s25;
            s25.next = s26;
            s26.next = s27;
            s27.next = s28;
            s28.next = s29;
            s29.next = s30;
            s30.next = s31;
            s31.next = s32;
            s32.next = s33;
            s33.next = s34;
            s34.next = s35;
            s35.next = s36;
            s36.next = s37;
            s37.next = s38;
            s38.next = s2;

            s2.prev = s38;
            s3.prev = s2;
            s4.prev = s3;
            s5.prev = s4;
            s6.prev = s5;
            s7.prev = s6;
            s8.prev = s7;
            s9.prev = s8;
            s10.prev = s9;
            s11.prev = s10;
            s12.prev = s11;
            s13.prev = s12;
            s14.prev = s13;
            s15.prev = s14;
            s16.prev = s15;
            s17.prev = s16;
            s18.prev = s17;
            s19.prev = s18;
            s20.prev = s19;
            s21.prev = s20;
            s22.prev = s21;
            s23.prev = s22;
            s24.prev = s23;
            s25.prev = s24;
            s26.prev = s25;
            s27.prev = s26;
            s28.prev = s27;
            s29.prev = s28;
            s30.prev = s29;
            s31.prev = s30;
            s32.prev = s31;
            s33.prev = s32;
            s34.prev = s33;
            s35.prev = s34;
            s36.prev = s35;
            s37.prev = s36;
            s38.prev = s37;

        }

        



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void label39_Click(object sender, EventArgs e)
        {

        }

        //ROLL
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Hide();
            //turn player 1
            int d1, d2;
            d1 = r.Next(6) + 1;
            d2 = r.Next(6) + 1;
            label48.Text = d1.ToString();
            label49.Text = d2.ToString();
            label48.Show();
            label49.Show();
            if (players[1].exhaust > 0)
            {
                players[1].exhaust--;
                richTextBox1.Text += Environment.NewLine + "Player 1 is resting.";
                //g << "Player " << turn << " is resting" << endl;
            }
            else
            {
                for (int moving = 0; moving < d1 + d2; moving++)
                {
                    players[1].currspace = players[1].currspace.next;
                    if (players[1].currspace == s38)
                    {
                        players[1].money += 3000;
                        banca -= 3000;
                        richTextBox1.Text += Environment.NewLine + "Player 1 passed go and collected 3000.";
                        //g << "Player " << turn << " passed go and collected 3000" << endl;
                    }
                }

                switch (players[1].currspace.type)
                {
                    case 1:
                        onprop(1, ref players[1].currspace);
                        break;
                    case 2:
                        onloto(1);
                        break;
                    case 3:
                        ontelex(1);
                        break;
                    case 4:
                        onconsfatuiri(1);
                        break;
                    case 5:
                        onbanca(1);
                        break;
                    case 6:
                        onmergilastart(1);
                        break;
                    case 7:
                        onodihna(1);
                        break;
                    case 8:
                        ondividente(1);
                        break;
                    default:
                        break;



                }

                if (!(players[1].money >= 0 && players[2].money >= 0 && players[3].money >= 0 && banca >= 0))
                {
                    end();
                    ending = true;
                }


            }
            label43.Text = players[1].money.ToString();
            label44.Text = players[2].money.ToString();
            label45.Text = players[3].money.ToString();
            label46.Text = banca.ToString();
            if (!ending)
            button7.Show();


        }

        //Start
        private void button2_Click(object sender, EventArgs e)
        {
            init();
            button8.Hide();
            richTextBox2.Hide();
            button2.Hide();
            for(int i = 1; i <= 3; i++)
            {
                players[i] = new Player(s1);
                
            }
            label43.Text = label44.Text = label45.Text = "85500";
            label46.Text = "4000";
            button1.Show();
            banca = 4000;
            currloto = 0;
            currtelex = 0;
            randomizearray(loto, 18);
            randomizearray(telex, 30);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        ///BUY YES
        private void button3_Click(object sender, EventArgs e)
        {
            players[1].currspace.owner = 1;
            players[1].money -= players[1].currspace.proprice;
            banca += players[1].currspace.proprice;
            
            button3.Hide();
            button4.Hide();
            label47.Hide();
            label43.Text = players[1].money.ToString();
            label46.Text = banca.ToString();
            richTextBox1.Text += Environment.NewLine + "Player" + 1 + " purchased " + players[1].currspace.propname + ". Rent is " + players[1].currspace.rent.ToString();
        }
        
        ///BUY NO
        private void button4_Click(object sender, EventArgs e)
        {
            button3.Hide();
            button4.Hide();
            label47.Hide();
            richTextBox1.Text += Environment.NewLine + "Player" + 1 + " landed " + players[1].currspace.propname + ", but did not purchase it.";

        }

        private void label47_Click(object sender, EventArgs e)
        {

        }
        //UPGRADE YES
        private void button5_Click(object sender, EventArgs e)
        {
            players[1].currspace.currupgrade++;
            players[1].money -= players[1].currspace.upgradeprice[players[1].currspace.currupgrade];
            banca += players[1].currspace.upgradeprice[players[1].currspace.currupgrade];
            players[1].currspace.rent += players[1].currspace.upgraderent[players[1].currspace.currupgrade];
            button5.Hide();
            button6.Hide();
            label47.Hide();
            richTextBox1.Text += Environment.NewLine + "Player" + 1 + " upgraded " + players[1].currspace.propname + ". Rent is" + players[1].currspace.rent.ToString();
        }
        //UPGRADE NO
        private void button6_Click(object sender, EventArgs e)
        {
            button5.Hide();
            button6.Hide();
            label47.Hide();
            richTextBox1.Text += Environment.NewLine + "Player" + 1 + " landed " + players[1].currspace.propname + ", but did not upgrade it.";
        }

        ///End turn
        private void button7_Click(object sender, EventArgs e)
        {
            button3.Hide();
            button4.Hide();
            button5.Hide();
            button6.Hide();
            button7.Hide();
            label47.Hide();
            for (int turn = 2; turn <= 3; turn++)
            {
                int d1, d2;
                d1 = r.Next(6) + 1;
                d2 = r.Next(6) + 1;
                if (players[turn].exhaust > 0)
                {
                    players[turn].exhaust--;
                    richTextBox1.Text += Environment.NewLine + "Player" + turn.ToString() + " is resting.";
                    //g << "Player " << turn << " is resting" << endl;
                }
                else
                {
                    for (int moving = 0; moving < d1 + d2; moving++)
                    {
                        players[turn].currspace = players[turn].currspace.next;
                        if (players[turn].currspace == s38)
                        {
                            players[turn].money += 3000;
                            banca -= 3000;
                            richTextBox1.Text += Environment.NewLine + "Player" + turn.ToString() + " passed go and collected 3000.";
                            //g << "Player " << turn << " passed go and collected 3000" << endl;
                        }
                    }

                    switch (players[turn].currspace.type)
                    {
                        case 1:
                            onprop(turn, ref players[turn].currspace);
                            break;
                        case 2:
                            onloto(turn);
                            break;
                        case 3:
                            ontelex(turn);
                            break;
                        case 4:
                            onconsfatuiri(turn);
                            break;
                        case 5:
                            onbanca(turn);
                            break;
                        case 6:
                            onmergilastart(turn);
                            break;
                        case 7:
                            onodihna(turn);
                            break;
                        case 8:
                            ondividente(turn);
                            break;
                        default:
                            break;



                    }

                    if (!(players[1].money >= 0 && players[2].money >= 0 && players[3].money >= 0 && banca >= 0))
                    {
                        end();
                        ending = true;
                    }


                }

            }
            if(!ending)
                button1.Show();

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        /// RULES
       
      
        private void button8_Click(object sender, EventArgs e)
        {
            richTextBox2.Show();
            button8.Hide();

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }
    }
}
