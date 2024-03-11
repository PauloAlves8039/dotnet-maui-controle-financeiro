using CommunityToolkit.Mvvm.Messaging;
using ControleFinanceiro.Models;
using ControleFinanceiro.Models.Enums;
using ControleFinanceiro.Repositories.Interfaces;
using System.Text;

namespace ControleFinanceiro.Views;

public partial class TransactionAdd : ContentPage
{
    private ITransactionRepository _repository;

    public TransactionAdd(ITransactionRepository repository)
	{
		InitializeComponent();
        _repository = repository;
	}

    private void TapGestureRecognizerTapped_ToClose(object sender, TappedEventArgs e)
    {
		Navigation.PopModalAsync();
    }

    private void OnButtonClicked_Save(object sender, EventArgs e)
    {
        if (IsValidData() == false) 
        {
            return;
        }

        SaveTransactionInDatabase();
        Navigation.PopModalAsync();

        WeakReferenceMessenger.Default.Send<string>(string.Empty);
    }

    private bool IsValidData() 
    {
        bool valid = true;
        double result;
        StringBuilder stringBuilder = new StringBuilder();

        if (string.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text)) 
        {
            stringBuilder.AppendLine("O campo 'Nome' deve ser preenchido!");
            valid = false;
        }

        if (string.IsNullOrEmpty(EntryValue.Text) || string.IsNullOrWhiteSpace(EntryValue.Text))
        {
            stringBuilder.AppendLine("O campo 'Valor' deve ser preenchido!");
            valid = false;
        }

        if (!string.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text, out result)) 
        {
            stringBuilder.AppendLine("O campo 'Valor' é inválido!");
            valid = false;
        }

        if (valid == false) 
        {
            LabelError.IsVisible = true;
            LabelError.Text = stringBuilder.ToString();
        }

        return valid;
    }

    private void SaveTransactionInDatabase() 
    {
        var transaction = new Transaction()
        {
            Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expense,
            Name =EntryName.Text,
            Date = DatePickerDate.Date,
            Value = Math.Abs(double.Parse(EntryValue.Text))
        };

        _repository.Add(transaction);
    }
}