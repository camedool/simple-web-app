namespace SimpleWebApp.WebApi.Dtos;

public record WarehouseDto(
    long Id, 
    string Name, 
    string Location, 
    long Capacity);
