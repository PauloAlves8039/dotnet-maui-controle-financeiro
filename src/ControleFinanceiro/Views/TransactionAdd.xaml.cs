using System.Text;

namespace ControleFinanceiro.Views;

public partial class TransactionAdd : ContentPage
{
	public TransactionAdd()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		Navigation.PopModalAsync();
    }

    private void OnButtonClicked_Save(object sender, EventArgs e)
    {

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

        if (string.IsNullOrEmpty(EntryValue.Text) || double.TryParse(EntryValue.Text, out result)) 
        {
            stringBuilder.AppendLine("O campo 'Valor' é inválido!");
            valid = false;
        }

        if (valid == false) 
        {
            LabelError.Text = stringBuilder.ToString();
        }

        return valid;
    }
}