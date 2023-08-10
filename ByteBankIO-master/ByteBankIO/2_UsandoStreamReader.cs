using ByteBankIO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Unicode;

partial class Program
{
    static void UsandoStreamReader(string[] args)
    {
        var enderecoDoArquivo = "contas.txt";
        using (var FluxoDeArquivos = new FileStream(enderecoDoArquivo, FileMode.Open))
        {
            var leitor = new StreamReader(FluxoDeArquivos);

            //var linha = leitor.ReadLine();
            //var texto = leitor.ReadToEnd();
            //var numero = leitor.Read();

            while (!leitor.EndOfStream)
            {
                var linha = leitor.ReadLine();
                var contaCorrente = ConverterStringParaContaCorrente(linha);

                var msg = $"{contaCorrente.Titular.Nome}: Conta número: {contaCorrente.Numero}, ag: {contaCorrente.Agencia}, saldo: {contaCorrente.Saldo}";
                Console.WriteLine(msg);
                leitor.ReadLine();
            }

        }
        Console.ReadLine();

    }

    static ContaCorrente ConverterStringParaContaCorrente(string linha)
    {
        // 375 4644 2483.13 Jonatan
        var campos = linha.Split(',');
        var agencia = campos[0];
        var numero = campos[1];
        var saldo = campos[2].Replace('.', ',');
        var nomeTitular = campos[3];

        var agenciaComInt = int.Parse(agencia);
        var numeroComInt = int.Parse(numero);
        var saldoComDouble = Double.Parse(saldo);
        var titular = new Cliente();
        titular.Nome = nomeTitular;

        var resultado = new ContaCorrente(agenciaComInt, numeroComInt);
        resultado.Depositar(saldoComDouble);
        resultado.Titular = titular;

        return resultado;
    }
}