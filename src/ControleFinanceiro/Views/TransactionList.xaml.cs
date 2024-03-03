using CommunityToolkit.Mvvm.Messaging;
using ControleFinanceiro.Models.Enums;
using ControleFinanceiro.Repositories.Interfaces;

namespace ControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
    private ITransactionRepository _repository;

    public TransactionList(ITransactionRepository repository)
	{
        _repository = repository;

        InitializeComponent();

        Reload();

        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) => 
        {
            Reload();
        });
	}

	private void OnButtonClicked_To_TransactionAdd(object sender, EventArgs args) 
	{
        var transactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
        Navigation.PushModalAsync(transactionAdd);
    }

    private void OnButtonClicked_To_TransactionEdit(object sender, EventArgs e)
    {
        var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
        Navigation.PushModalAsync(transactionEdit);
    }

    private void Reload() 
    {
        var items = _repository.GetAll();
        CollectionViewTransactions.ItemsSource = items;

        var income = items.Where(a => a.Type == TransactionType.Income).Sum(a => a.Value);
        var expense = items.Where(a => a.Type == TransactionType.Expense).Sum(a => a.Value);
        var balance = income - expense;

        LabelIncome.Text = income.ToString("C");
        LabelExpense.Text = expense.ToString("C");
        LabelBalance.Text = balance.ToString("C");

    } 
}