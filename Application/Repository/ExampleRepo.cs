/* public async Task<IEnumerable<object>> MascotasVacunadas()
    {
        int año = 2023;
        DateOnly trimestreInicio = new DateOnly(año, 1, 1);
        DateOnly trimestreFinal = new DateOnly(año, 3, 31);

        var mascotas = await (
                        from c in _context.Citas
                        join m in _context.Mascotas on c.MascotaIdFk equals m.Id
                        where c.Motivo == "Vacunacion" && c.Fecha >= trimestreInicio && c.Fecha <= trimestreFinal
                        select new
                        {
                            Nombre = m.Nombre,
                            Motivo = c.Motivo,
                            FechaCita = c.Fecha,
                            Veterinario = c.Veterinario.Nombre
                        }).Distinct()
                        .ToListAsync();
        return mascotas;
    } */

/*  public async Task<object> MascotasPorEspecie()
    {
        var consulta =
        from e in _context.Especies
        select new
        {
            Especie = e.Nombre,
            Mascotas = (from mas in _context.Mascotas
                        join r in _context.Razas on mas.RazaIdFk equals r.Id
                        where mas.RazaIdFk == r.Id
                        where r.EspecieIdFk == e.Id
                        select new
                        {
                            Nombre = mas.Nombre,
                            Especie=r.Especie.Nombre,
                            Raza = r.Nombre
                        }).ToList()
        };

        var MasEspecie = await consulta.ToListAsync();
        return MasEspecie;

 } */


