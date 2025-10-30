using Prometheus;

namespace MVCSPortTime1.Monitoring
{
    public static class AppMetrics
    {
        private static readonly Counter TurnosReservadosTotal = Metrics.CreateCounter(
            "turnos_reservados_total",
            "Total acumulado de turnos reservados");

        private static readonly Counter TurnosReservadosPorDeporte = Metrics.CreateCounter(
            "turnos_reservados_por_deporte_total",
            "Turnos reservados por tipo de deporte",
            new CounterConfiguration { LabelNames = new[] { "deporte" } });

        private static readonly Counter Errores = Metrics.CreateCounter(
            "app_errores_total",
            "Cantidad de errores por tipo (500, validacion, etc.)",
            new CounterConfiguration { LabelNames = new[] { "tipo" } });

        public static void ReservaCreada(string deporte)
        {
            TurnosReservadosTotal.Inc();
            TurnosReservadosPorDeporte.WithLabels(string.IsNullOrWhiteSpace(deporte) ? "desconocido" : deporte).Inc();
        }

        public static void Error(string tipo)
        {
            Errores.WithLabels(string.IsNullOrWhiteSpace(tipo) ? "desconocido" : tipo).Inc();
        }
    }
}
