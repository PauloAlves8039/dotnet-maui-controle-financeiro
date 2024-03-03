using CommunityToolkit.Mvvm.Messaging;
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
        CollectionViewTransactions.ItemsSource = _repository.GetAll();
    } 
}