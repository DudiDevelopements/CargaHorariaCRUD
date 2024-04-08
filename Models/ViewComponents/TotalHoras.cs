using CargaHorariaCRUD.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CargaHorariaCRUD.Models.ViewComponents {
    public class TotalHoras : BaseViewComponent {
        public TotalHoras(IEnvioRepository envioRepository) : base(envioRepository) { }

        public async Task<IViewComponentResult> InvokeAsync() {
            int? cargaHorariaEmMinutos = GetCargaHorariaTotal();

            return View(cargaHorariaEmMinutos);
        }
    }
}
