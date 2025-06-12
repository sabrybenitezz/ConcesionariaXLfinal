

namespace ConcesionariaVehiculos.Models;

/// <summary>
/// Clase cliente.
/// </summary>
public class Cliente
{
    /// <summary>
    /// Identificador único del cliente.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Nombre del cliente.
    /// </summary>
    public string Nombre { get; set; }
    /// <summary>
    /// Apellido del cliente.
    /// </summary>
    public string Apellido { get; set; }
    /// <summary>
    /// correo eletronico del cliente.
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Numero de telefono del cliente.
    /// </summary>
    public string Telefono { get; set; }


}
