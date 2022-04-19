using PizzeriaContracts.BindingModels;
using PizzeriaContracts.StoragesContracts;
using PizzeriaContracts.ViewModels;
using PizzeriaDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaDatabaseImplement.Implements
{
    public class ImplementerStorage : IImplementerStorage
    {
        public List<ImplementerViewModel> GetFullList()
        {
            using var context = new PizzeriaDatabase();
            return context.Implementers
                .Select(CreateModel)
                .ToList();
        }
        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new PizzeriaDatabase();
            return context.Implementers
            .Include(rec => rec.Orders)
            .Where(rec => rec.ImplementerFIO == model.ImplementerFIO)
            .Select(CreateModel)
            .ToList();
        }
        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new PizzeriaDatabase();
            var implementer = context.Implementers
            .Include(rec => rec.Orders)
            .FirstOrDefault(rec => rec.ImplementerFIO == model.ImplementerFIO || rec.Id == model.Id);
            return implementer != null ? CreateModel(implementer) : null;
        }
        public void Insert(ImplementerBindingModel model)
        {
            using var context = new PizzeriaDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Implementers.Add(CreateModel(model, new Implementer()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(ImplementerBindingModel model)
        {
            using var context = new PizzeriaDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Исполнитель не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(ImplementerBindingModel model)
        {
            using var context = new PizzeriaDatabase();
            Implementer element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Implementers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Исполнитель не найден");
            }
        }
        private static Implementer CreateModel(ImplementerBindingModel model, Implementer implementer)
        {
            implementer.ImplementerFIO = model.ImplementerFIO;
            implementer.WorkingTime = model.WorkingTime;
            implementer.PauseTime = model.PauseTime;
            return implementer;
        }
        private static ImplementerViewModel CreateModel(Implementer implementer)
        {
            return new ImplementerViewModel
            {
                Id = implementer.Id,
                ImplementerFIO = implementer.ImplementerFIO,
                WorkingTime = implementer.WorkingTime,
                PauseTime = implementer.PauseTime
            };
        }
    }
}
