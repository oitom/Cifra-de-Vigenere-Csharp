using System;                               /***********************************/
using System.Collections.Generic;           /** Autor: Wellington             **/
using System.ComponentModel;                /** Disciplina: GSD, Prof. Joao   **/
using System.Data;                          /** Criptografia de Vigenere      **/
using System.Drawing;                       /***********************************/
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Cripto(object sender, EventArgs e)
        {
            //virificação de campos vazios
            error.Clear();
            bool erro = false;
           
            if (textBoxChave.Text == "")
            {
                error.SetError(textBoxChave, "Não esta preenchido!");
                erro = true;
            }        
            if (richTextBoxMenssagen.Text == "")
            {
                error.SetError(richTextBoxMenssagen, "Não esta preenchido!");
                erro = true;
            }
            
            if(textBoxChave.Text != "" && richTextBoxMenssagen.Text !="")
                erro = false;

            //se os campos forem preenchidos executa a criptografia
            if (erro == false)
            {

                char[] alfabeto = new char[26] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

                List<int> ListaChave = new List<int>();
                List<int> ListaMensagem = new List<int>();

                string chave = textBoxChave.Text.ToLower().Trim();
                chave = chave.Replace(" ", "");
                string mensagem = richTextBoxMenssagen.Text.ToLower().Trim();

                string cripto = " ";

                for (int i = 0; i < mensagem.Length; i++)
                {
                    //verifica se na mensagem tem um espaço em branco
                    if (mensagem[i] == ' ')
                        ListaMensagem.Add(32);

                    //percorre o alfabeto
                    for (int j = 0; j < alfabeto.Length; j++)
                    {
                        //verifica se a letra da mensagem é igual a do alfabeto
                        //caso seja quarda na ListMensagem a posicao da letra situada no alfabeto
                        if (mensagem[i] == alfabeto[j])
                            ListaMensagem.Add(j);

                    }
                }

                int aux = 0;
                
                for (int i = 0; i < ListaMensagem.Count; i++)
                {
                    //verifica se na mensagem tem um espaço em branco
                    //se tiver adiciona também na mesma posição na listaChave
                    if (ListaMensagem[i] == 32)
                        ListaChave.Add(32); 

                    for (int j = 0; j < alfabeto.Length; j++)
                    {
                        //verifica se o tamnho da mensagem é menor que o tamanho da chave
                        //ou se estorou o indice da chave[i]
                        if (i < chave.Length)
                        {
                            //verifica se a letra da chave é igual a do alfabeto
                            //caso seja quarda no ListChave a posicao da letra situada no alfabeto
                            if (chave[i] == alfabeto[j])
                                ListaChave.Add(j);
                        }
                        else
                        {                            
                            //caso tenha estorado o ince da chave[i], a variavel "aux" é quem controla a lista
                            //verifica se o aux é menor que a mensagem
                            if (aux < ListaMensagem.Count)
                            {

                                //verifica se na mensagem tem um espaço em branco
                                //se tiver adiciona também na mesma posição na listaChave
                                if (ListaMensagem[aux] == 32)
                                    ListaChave.Add(32);
                            
                                //verifica se as listas são do mesmo tamanho
                                if (ListaChave.Count != ListaMensagem.Count)
                                {
                                    //verifica se o aux esta dentro dos limites da chave
                                    if (aux < chave.Length)
                                    {
                                        //como a mensagem é maior que a chave adiciona repetidamente até que os dois tenha o mesmo tamanho
                                        //verifica se a letra da chave é igual a do alfabeto caso seja quarda no ListChave a posicao da letra 
                                        //situada no alfabeto
                                        if (chave[aux] == alfabeto[j])
                                        {
                                            ListaChave.Add(j);
                                            aux++;
                                        }
                                    }
                                    else
                                        aux = 0;
                                }

                            }

                        }//12 20 8 19 14
                    }
                }

                for (int i = 0; i < ListaMensagem.Count; i++)
                {
                    //verifica se na ListaMensagem tem um "espaço" = 32 no ASCII
                    //caso tenha adiciona no resultado da cripto
                    if (ListaMensagem[i] == 32 && ListaChave[i] == 32)
                        cripto += " ";
                    else
                    {
                        //aplica a formula da criptografia 
                        int posicao = (ListaMensagem[i] + ListaChave[i]) % 26;
                        cripto += alfabeto[posicao];
                    }
                }
                //mostra o resultado da criptografia
                richTextBoxSaida.Text = cripto;
            }
        }
        //Limpar os campos
        private void button2_Click(object sender, EventArgs e)
        {
            textBoxChave.Text = "";
            richTextBoxMenssagen.Text = "";
            richTextBoxSaida.Text = "";
            error.Clear();
        }
        
    }
}
