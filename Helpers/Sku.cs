namespace web.Entities;

//stock keeping Unit
public record Sku
{
    private const int Defaultlength = 15 ;

    private Sku(string value)=> value = value;

    public static Sku? Create(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            return null;
        }

        if(value.Length != Defaultlength)
        {
            return null ;
        }
        return new Sku(value);
    }
}