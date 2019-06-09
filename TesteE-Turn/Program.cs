using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteE_Turn.Modelos;

namespace TesteE_Turn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Os arquivos de testes encontram-se dentro da pasta 'Debug' do projeto, precione qualquer tecla para continuar!");
            Console.ReadKey();
            List<Palestra> Palestras = new List<Palestra>();
            Trilha[] Trilhas = new Trilha[2];
            LerArquivo(Palestras);
            DateTime data;
            CalcularAlocacao(Palestras, Trilhas);           
            for (int i = 0; i < Trilhas.Length; i++)
            {
                data = new DateTime(2019, 06, 9, 9, 0, 0);               
                Console.WriteLine("----------------Trilha " + (i + 1) + " --------------");                
                foreach (Palestra Palestra in Trilhas[i].PalestrasManha)
                {
                    Console.WriteLine(string.Format("{0}:{1} - {2}", data.Hour, data.Minute, (Palestra.Descricao + " " + Palestra.Duracao+ "Min")));
                    data = data.AddMinutes(Convert.ToDouble(Palestra.Duracao));
                }
                Console.WriteLine("12:00 - Almoço");
                data = data.AddMinutes(60);                ;
                foreach (Palestra Palestra in Trilhas[i].PalestrasTarde)
                {
                    Console.WriteLine(string.Format("{0}:{1} - {2}", data.Hour, data.Minute, (Palestra.Descricao + " " + Palestra.Duracao + "Min")));
                    data = data.AddMinutes(Convert.ToDouble(Palestra.Duracao)); ;
                }
                Console.WriteLine("17:00 - Evento de Networking");
            }
            Console.ReadKey();
        }
        public static void LerArquivo(List<Palestra> Palestras)
        {
            try
            {
                using (StreamReader sr = new StreamReader("Entradas.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] Dados = line.Split(';');
                        Palestra palestra = new Palestra
                        {
                            Descricao = Dados[0],
                            Duracao = decimal.Parse(Dados[1])
                        };
                        Palestras.Add(palestra);
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Arquivo não encontrado:");
                Console.WriteLine(e.Message);
            }
        }
        public static void CalcularAlocacao(List<Palestra> Palestras, Trilha[] Trilhas)
        {
            for (int i = 0; i < Trilhas.Length; i++)
            {

                List<Palestra> Aux = new List<Palestra>();
                Trilhas[i] = new Trilha();
                decimal TotalManha = 0m;
                decimal TotalTarde = 0m;
                foreach (Palestra item in Palestras)
                {
                    TotalManha += item.Duracao;
                    Aux.Add(item);
                    if (TotalManha == 180 || TotalManha > 180)
                    {
                        if (TotalManha > 180)
                        {
                            TotalManha -= Aux.Last().Duracao;
                            var TotalFaltante = 180 - TotalManha;
                            Aux.RemoveAt(Aux.Count - 1);
                            var Possibilidade = Palestras.Where(x => x.Duracao == TotalFaltante).FirstOrDefault();
                            if (Possibilidade == null)
                            {
                                TotalManha -= Aux.Last().Duracao;
                                Aux.RemoveAt(Aux.Count - 1);
                            }

                        }
                        else
                        {
                            foreach (Palestra remover in Aux)
                            {
                                Trilhas[i].PalestrasManha.Add(remover);
                                Palestras.Remove(remover);
                            }
                            break;
                        }
                    }
                    else
                    {

                    }
                }
                Aux = new List<Palestra>();
                foreach (Palestra item in Palestras)
                {
                    TotalTarde += item.Duracao;
                    Aux.Add(item);
                    if (TotalTarde == 240 || TotalTarde > 240)
                    {
                        if (TotalTarde > 240)
                        {
                            TotalTarde -= Aux.Last().Duracao;
                            var TotalFaltante = 240 - TotalTarde;
                            Aux.RemoveAt(Aux.Count - 1);
                            var Possibilidade = Palestras.Where(x => x.Duracao == TotalFaltante).FirstOrDefault();
                            if (Possibilidade == null)
                            {
                                TotalTarde -= Aux.Last().Duracao;
                                Aux.RemoveAt(Aux.Count - 1);
                            }
                        }
                        else
                        {
                            foreach (Palestra remover in Aux)
                            {
                                Trilhas[i].PalestrasTarde.Add(remover);
                                Palestras.Remove(remover);
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
