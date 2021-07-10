using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApiConsultarCnpj.Properties;

using static ApiConsultarCnpj.DadosCnpj;

namespace ApiConsultarCnpj
{
    public partial class Form1 : Form
    {
        List<string> DadosCnpj = new List<string>();
        public Form1()
        {
            InitializeComponent();
            
        }

        string cnpj;
        

        private void button1_Click(object sender, EventArgs e)
        {
            cnpj = maskedTextBox1.Text;
            var result = cnpj;

            if (result != null)
            {
                var requisicao = HttpWebRequest.CreateHttp("https://www.receitaws.com.br/v1/cnpj/" + cnpj); // Criação do objeto que será utilizado para criar a requisição
                requisicao.Method = "GET"; // Passando o método GET para q requisão
                requisicao.UserAgent = "Requisicao"; // Header do HTTP para evitar o erro 403

                using (var resposta = requisicao.GetResponse()) // Obtendo a resposta da requisição
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();

                    var post = JsonConvert.DeserializeObject<Cnpj>(objResponse.ToString());
                    if (post.status.Equals("OK"))
                    {
                        DadosCnpj.Add(post.cnpj);
                        DadosCnpj.Add(post.nome);                        
                        DadosCnpj.Add(post.atividade_principal[0].code);
                        DadosCnpj.Add(post.atividade_principal[0].text);
                        DadosCnpj.Add(post.fantasia);
                        DadosCnpj.Add(post.logradouro);
                        DadosCnpj.Add(post.numero);
                        DadosCnpj.Add(post.cep);
                        DadosCnpj.Add(post.bairro);
                        DadosCnpj.Add(post.municipio);
                        DadosCnpj.Add(post.uf);
                       
                        Cnpj.Text = post.cnpj;
                        textBox1.Text = post.nome;
                        textBox2.Text = post.atividade_principal[0].code;
                        textBox3.Text = post.atividade_principal[0].text;                        
                        Fantasia.Text = post.fantasia;
                        textBox4.Text = post.logradouro;
                        textBox5.Text = post.numero;
                        textBox6.Text = post.bairro;
                        textBox7.Text = post.municipio;
                        textBox8.Text = post.uf;
                        
                       
                    }
                    else
                    {
                        MessageBox.Show("Dados incorretos digite novamente");
                    }
                }
            }           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            maskedTextBox1.Text = string.Empty;
            Cnpj.Text = string.Empty;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            Fantasia.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }      
        private void Dados_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        
    }
}
