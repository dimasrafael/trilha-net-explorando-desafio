using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;


// Cria os modelos de hóspedes e cadastra na lista de hóspedes
List<Pessoa> hospedes = new List<Pessoa>();
List<Reserva> reservas = new List<Reserva>();
List<Suite> suites = new List<Suite>();

string breakLine = "==========================================";

// Inicio do menu do sistema de hospedagem
string opcao;
bool exibirMenu = true;
int idSuite = 1;
string opcaoSuite;
while (exibirMenu)
{
    Console.WriteLine("Digite a opção desejada:");
    Console.WriteLine(breakLine);
    Console.WriteLine("1 - Cadastrar hospede");
    Console.WriteLine("2 - Cadastrar suite");
    Console.WriteLine("3 - Reservar hospedagem");
    Console.WriteLine("4 - Listar Reservas de hospedagem");
    Console.WriteLine("5 - Encerrar Programa");
    Console.WriteLine(breakLine);

    opcao = Console.ReadLine();
    switch (opcao)
    {
        case "1":
            Console.WriteLine("Digite o nome do hospede: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o sobrenome do hospede: ");
            string sobreNome = Console.ReadLine();
            try
            {   
                if (sobreNome != "")
                {
                    Pessoa novaPessoa = new Pessoa(nome: nome, sobrenome: sobreNome);
                    hospedes.Add(novaPessoa);
                    Console.WriteLine($"Hospede {nome} {sobreNome} cadastrado com sucesso");
                    Console.WriteLine(breakLine);
                }
                else
                {
                    Pessoa novaPessoa = new Pessoa(nome: nome);
                    hospedes.Add(novaPessoa);
                    Console.WriteLine($"Hospede {nome} cadastrado com sucesso");
                    Console.WriteLine(breakLine);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar, tente novamente");
                Console.WriteLine(breakLine);
            }
            
            
            
            break;
        case "2":
            Console.WriteLine("Digite o tipo de suite: ");
            string tipo = Console.ReadLine();
            Console.WriteLine("Digite a capacidade da suite:");
            string capacidade = Console.ReadLine();
            Console.WriteLine("Digite o valor da diaria:");
            string valorDiaria = Console.ReadLine();

            try
            {
                int.TryParse(capacidade, out int capacidadeNumerica);
                int.TryParse(valorDiaria, out int valorDiariaNumerico);
                Suite novaSuite = new Suite(tipo, capacidadeNumerica, valorDiariaNumerico);
                suites.Add(novaSuite);
                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex}");
                Console.WriteLine(breakLine);
            }
            
            
            
            break;
        case "3":

            if(hospedes.Count == 0 )
            {
                Console.WriteLine(breakLine);
                Console.WriteLine("Favor cadastrar os hospedes antes da reserva");
                Console.WriteLine(breakLine);
                break;
            }
            else if(suites.Count == 0)
            {
                Console.WriteLine(breakLine);
                Console.WriteLine("Favor cadastrar a suite antes da reserva");
                Console.WriteLine(breakLine);
                break;
            }
            else
            {
                try
                {
                    Console.WriteLine("Selecione a suite:");
                    foreach (Suite suite in suites)
                    {
                        Console.WriteLine($"ID: {idSuite} - {suite.TipoSuite} - {suite.Capacidade} - {suite.ValorDiaria}");
                        idSuite++;
                    }
                    opcaoSuite = Console.ReadLine();
                    int opcaoSuiteNum = int.Parse(opcaoSuite);
                    Suite suiteSelecionada = new Suite(suites[opcaoSuiteNum - 1].TipoSuite, suites[opcaoSuiteNum - 1].Capacidade, suites[opcaoSuiteNum - 1].ValorDiaria);
                    Console.WriteLine("Digite a quantidade de dias para reservar:");
                    string dias = Console.ReadLine();
                    int diasNumerico = int.Parse(dias);
                    Reserva reserva = new Reserva(diasNumerico);
                    reserva.CadastrarSuite(suiteSelecionada);
                    reserva.CadastrarHospedes(hospedes);
                    reservas.Add(reserva);

                    //reserva = null;
                    hospedes.Clear();
                    idSuite = 1;

                    Console.WriteLine("Reserva cadastrada com sucesso!");
                    Console.WriteLine(breakLine);
                }
                
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro {ex}");
                }
            }







                break;
        case "4":
            if(reservas.Count > 0)
            {
                foreach(Reserva reserva in reservas)
                {
                    Console.WriteLine(breakLine);
                    Console.WriteLine(reserva.ObterHospedes()); // Exibe os hóspedes
                    Console.WriteLine($"Suíte: {reserva.Suite.TipoSuite}"); // Exibe o tipo da suíte
                    Console.WriteLine($"Capacidade da Suíte: {reserva.Suite.Capacidade}"); // Exibe a capacidade da suíte
                    Console.WriteLine($"Valor da Diária: {reserva.Suite.ValorDiaria:C}"); // Exibe o valor da diária
                    Console.WriteLine($"Dias Reservados: {reserva.DiasReservados}"); // Exibe os dias reservados
                    Console.WriteLine($"Valor Total da Reserva: {reserva.CalcularValorDiaria():C}"); // Exibe o valor total da reserva
                    Console.WriteLine(breakLine);
                }
                    
            }
            else
            {
                Console.WriteLine("Não há reservas cadastradas");
            }
                break;
        case "5":
            Console.WriteLine("Encerrando...");
            exibirMenu = false;
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;




    }
}






//Pessoa p1 = new Pessoa(nome: "Hóspede 1");
//Pessoa p2 = new Pessoa(nome: "Hóspede 2");

//hospedes.Add(p1);
//hospedes.Add(p2);

//// Cria a suíte
//Suite suite = new Suite(tipoSuite: "Premium", capacidade: 2, valorDiaria: 30);

//// Cria uma nova reserva, passando a suíte e os hóspedes
//Reserva reserva = new Reserva(diasReservados: 20);
//reserva.CadastrarSuite(suite);
//reserva.CadastrarHospedes(hospedes);

//// Exibe a quantidade de hóspedes e o valor da diária
//Console.WriteLine($"Hóspedes: {reserva.ObterQuantidadeHospedes()}");
//Console.WriteLine($"Valor diária: {reserva.CalcularValorDiaria()}");