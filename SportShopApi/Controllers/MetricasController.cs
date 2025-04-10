using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace SportshopApi.Controllers;

/// <summary>
/// API para obtener métricas de los productos deportivos.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MetricasController : ControllerBase
{
    private readonly IMetricsService _metricasService; 

    /// <summary>
    /// Constructor del MetricasController.
    /// </summary>
    /// <param name="metricasService">Servicio para el cálculo de métricas.</param>
    public MetricasController(IMetricsService metricasService)
    {
        _metricasService = metricasService;
    }

    /// <summary>
    /// Obtiene las métricas de los productos registrados.
    /// </summary>
    /// <returns>Un objeto JSON con el total de productos, las categorías principales, el stock total y el precio promedio.</returns>
    [HttpGet("productos/metricas")]
    public async Task<ActionResult<object>> GetProductoMetrics()
    {
        var metrics = await _metricasService.GetProductMetricsAsync();
        return Ok(metrics);
    }
}
