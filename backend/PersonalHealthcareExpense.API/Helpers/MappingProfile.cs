using AutoMapper;
using PersonalHealthcareExpense.API.DTOs;
using PersonalHealthcareExpense.API.DTOs.Expense;
using PersonalHealthcareExpense.API.Models;
using PersonalHealthcareExpense.API.DTOs.Income;
using PersonalHealthcareExpense.API.DTOs.Healthcare;
using PersonalHealthcareExpense.API.DTOs.Medicine;

namespace PersonalHealthcareExpense.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            // Expense
            CreateMap<Expense, ExpenseDTO>();
            CreateMap<AddExpenseDTO, Expense>();
            CreateMap<UpdateExpenseDTO, Expense>();

            //income
            CreateMap<Income, IncomeDTO>();

            CreateMap<AddIncomeDTO, Income>();

            CreateMap<UpdateIncomeDTO, Income>();

            //health care
            CreateMap<Healthcare, HealthcareDTO>();

            CreateMap<AddHealthcareDTO, Healthcare>();

            CreateMap<UpdateHealthcareDTO, Healthcare>();

            //medicin
            CreateMap<Medicine, MedicineDTO>();

            CreateMap<AddMedicineDTO, Medicine>();

            CreateMap<UpdateMedicineDTO, Medicine>();
        }

    }
}