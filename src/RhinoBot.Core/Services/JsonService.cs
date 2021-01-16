
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

public class JsonService
{
    public async Task<T> ReadAsync<T>(string fileName)
    {
        FileStream jsonString = await Task.Run<FileStream>(() => File.OpenRead(fileName));
        T data = await JsonSerializer.DeserializeAsync<T>(jsonString);
        jsonString.Close();
        return data;
    }

    public async Task WriteAsync<T>(T data, string fileName)
    {
        using FileStream fs = File.Create(fileName);
        await JsonSerializer.SerializeAsync<T>(fs, data);
    }
}