using LunaPark; // Ajoutez ici le namespace où UIMenu et UIMenuCheckboxItem sont définis
// OU si elles sont dans un autre namespace, par exemple :
// using Client.net.LunaPark.UI;

namespace Client.net.LunaPark
{
	public delegate void CheckboxChangeEvent(UIMenu sender, UIMenuCheckboxItem checkboxItem, bool Checked);
}
