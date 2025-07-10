[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create(Insuree insuree)
{
    if (ModelState.IsValid)
    {
        decimal quote = 50m;

        int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
        if (insuree.DateOfBirth > DateTime.Now.AddYears(-age)) age--;

        if (age <= 18)
        {
            quote += 100m;
        }
        else if (age >= 19 && age <= 25)
        {
            quote += 50m;
        }
        else
        {
            quote += 25m;
        }

   
        if (insuree.CarYear < 2000)
        {
            quote += 25m;
        }
        else if (insuree.CarYear > 2015)
        {
            quote += 25m;
        }

        if (insuree.CarMake?.ToLower() == "porsche")
        {
            quote += 25m;
            if (insuree.CarModel?.ToLower() == "911 carrera")
            {
                quote += 25m;
            }
        }

        quote += insuree.SpeedingTickets * 10m;

        if (insuree.DUI)
        {
            quote *= 1.25m;
        }

        if (insuree.CoverageType)
        {
            quote *= 1.50m;
        }

        insuree.Quote = quote;

        db.Insurees.Add(insuree);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    return View(insuree);
}
