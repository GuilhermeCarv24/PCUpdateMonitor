Console.WriteLine("================================");
Console.WriteLine("         PC UPDATE MONITOR");
Console.WriteLine("================================");
Console.WriteLine();

VerificarAtualizacoes();


static void VerificarAtualizacoes()
{
    Console.WriteLine("Verificando atualizações do Windows...");
    Console.WriteLine();

    try
    {
        Type? tipoSessao = Type.GetTypeFromProgID("Microsoft.Update.Session");

        if (tipoSessao == null)
        {
            Console.WriteLine("Não foi possível acessar o Windows Update.");
            return;
        }

        dynamic sessao = Activator.CreateInstance(tipoSessao)!;

        dynamic pesquisador = sessao.CreateUpdateSearcher();

        dynamic resultado = pesquisador.Search(
            "IsInstalled=0 and Type='Software'"
        );

        int quantidade = resultado.Updates.Count;

        Console.WriteLine("--------------------------------");
        Console.WriteLine($"Atualizações encontradas: {quantidade}");
        Console.WriteLine("--------------------------------");

        if (quantidade == 0)
        {
            Console.WriteLine("Seu Windows está atualizado!");
        }
        else
        {
            Console.WriteLine("Existem atualizações disponíveis:");
            Console.WriteLine();

            for (int i = 0; i < quantidade; i++)
            {
                Console.WriteLine($"{i + 1}. {resultado.Updates[i].Title}");
            }
        }
    }
    catch (Exception erro)
    {
        Console.WriteLine("Ocorreu um erro ao consultar o Windows Update.");
        Console.WriteLine($"Detalhes: {erro.Message}");
    }
}