using System.Text.Json;
using Microsoft.Toolkit.Uwp.Notifications;

var client = new HttpClient();
string availableitensstring = "";

bool notify = false;

client.DefaultRequestHeaders.Add("User-Agent", "Goleada App");

//Request Body
var content = new MultipartFormDataContent();

content.Add(new StringContent("destaques"), "ordem");
content.Add(new StringContent(""), "busca");
content.Add(new StringContent("bd_store"), "BD_qual");

//Request
var response = await client.PostAsync("https://goleada.coritiba.com.br/dados/bd_store.php", content);

//Request Response
var responseString = await response.Content.ReadAsStringAsync();
responseString = responseString.Replace("\\", "");
responseString = responseString.Replace("\"[", "[");
responseString = responseString.Replace("]\"", "]");
//Stream stream = await response.Content.ReadAsStreamAsync();

AvailableItens availableitens = JsonSerializer.Deserialize<AvailableItens>(responseString)!;

//Printing the Response
//Console.Write(responseString);
foreach (var itens in availableitens.resultado!)
{
    if (itens.esgotado == "Esgotado")
    {
        availableitensstring += ($"Produto disponível: {itens.sfi_descricao}") + Environment.NewLine;
    
        notify = true;
    }
//Console.Write(itens.sfi_descricao);
}

if (notify)
{
    new ToastContentBuilder()
    .AddText("Novos Itens Disponíveis!")
    .AddText(availableitensstring)
    .Show();

    await Task.Delay(1000);
}

//Console.Write(availableitens.resultado);

