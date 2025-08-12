using Danger_Money;
using Microsoft.EntityFrameworkCore;

namespace Danger_Money.Infra.Data
{
    public static class DataSeeder
    {
        public static async Task SeedExpensesAsync(RepositoryDbContext context)
        {
            // Ensure the database is created and migrations are applied
            await context.Database.MigrateAsync();

            if (!context.Expenses.Any())
            {
                var expenses = new List<Expense>
                {
                    // Sample data for a retail company
                    new Expense { Name = "Aluguel da Loja Principal", Amount = 5000.00m, Date = DateTime.Now.AddDays(-5), Type = ExpenseTypeEnum.Aluguel },
                    new Expense { Name = "Salários - Equipe de Vendas", Amount = 15000.00m, Date = DateTime.Now.AddDays(-3), Type = ExpenseTypeEnum.Salarios },
                    new Expense { Name = "Campanha de Marketing Digital", Amount = 2500.00m, Date = DateTime.Now.AddDays(-10), Type = ExpenseTypeEnum.Marketing },
                    new Expense { Name = "Conta de Luz - Mês Anterior", Amount = 800.00m, Date = DateTime.Now.AddDays(-7), Type = ExpenseTypeEnum.Utilidades },
                    new Expense { Name = "Manutenção de Equipamentos", Amount = 1200.00m, Date = DateTime.Now.AddDays(-15), Type = ExpenseTypeEnum.Manutencao },
                    new Expense { Name = "Compra de Suprimentos de Escritório", Amount = 300.00m, Date = DateTime.Now.AddDays(-2), Type = ExpenseTypeEnum.Suprimentos },
                    new Expense { Name = "Frete de Mercadorias - Fornecedor A", Amount = 750.00m, Date = DateTime.Now.AddDays(-8), Type = ExpenseTypeEnum.Frete },
                    new Expense { Name = "Imposto sobre Vendas", Amount = 3500.00m, Date = DateTime.Now.AddDays(-1), Type = ExpenseTypeEnum.Impostos },
                    new Expense { Name = "Seguro da Loja", Amount = 600.00m, Date = DateTime.Now.AddDays(-20), Type = ExpenseTypeEnum.Seguros },
                    new Expense { Name = "Viagem de Negócios - Feira do Setor", Amount = 1800.00m, Date = DateTime.Now.AddDays(-12), Type = ExpenseTypeEnum.Viagens },
                    new Expense { Name = "Comissões de Vendas - Mês Atual", Amount = 4000.00m, Date = DateTime.Now.AddDays(-4), Type = ExpenseTypeEnum.Comissoes },
                    new Expense { Name = "Outras Despesas Operacionais", Amount = 900.00m, Date = DateTime.Now.AddDays(-6), Type = ExpenseTypeEnum.Outros },
                    new Expense { Name = "Aluguel do Depósito", Amount = 2000.00m, Date = DateTime.Now.AddDays(-25), Type = ExpenseTypeEnum.Aluguel },
                    new Expense { Name = "Salários - Equipe Administrativa", Amount = 8000.00m, Date = DateTime.Now.AddDays(-3), Type = ExpenseTypeEnum.Salarios },
                    new Expense { Name = "Anúncios em Redes Sociais", Amount = 1500.00m, Date = DateTime.Now.AddDays(-10), Type = ExpenseTypeEnum.Marketing },
                    new Expense { Name = "Conta de Água e Esgoto", Amount = 250.00m, Date = DateTime.Now.AddDays(-7), Type = ExpenseTypeEnum.Utilidades },
                    new Expense { Name = "Reparo de Fachada", Amount = 700.00m, Date = DateTime.Now.AddDays(-18), Type = ExpenseTypeEnum.Manutencao },
                    new Expense { Name = "Material de Limpeza", Amount = 150.00m, Date = DateTime.Now.AddDays(-2), Type = ExpenseTypeEnum.Suprimentos },
                    new Expense { Name = "Frete de Devoluções", Amount = 200.00m, Date = DateTime.Now.AddDays(-9), Type = ExpenseTypeEnum.Frete },
                    new Expense { Name = "Imposto de Renda Pessoa Jurídica", Amount = 6000.00m, Date = DateTime.Now.AddDays(-1), Type = ExpenseTypeEnum.Impostos },
                    new Expense { Name = "Seguro de Carga", Amount = 350.00m, Date = DateTime.Now.AddDays(-22), Type = ExpenseTypeEnum.Seguros },
                    new Expense { Name = "Passagens Aéreas - Reunião Fornecedor", Amount = 1000.00m, Date = DateTime.Now.AddDays(-14), Type = ExpenseTypeEnum.Viagens },
                    new Expense { Name = "Bônus de Desempenho", Amount = 2000.00m, Date = DateTime.Now.AddDays(-4), Type = ExpenseTypeEnum.Comissoes },
                    new Expense { Name = "Taxas Bancárias", Amount = 100.00m, Date = DateTime.Now.AddDays(-6), Type = ExpenseTypeEnum.Outros }
                };

                await context.Expenses.AddRangeAsync(expenses);
                await context.SaveChangesAsync();
            }
        }
    }
}
