using LunaPark; // Ajoutez ici le namespace o� UIMenu et UIMenuCheckboxItem sont d�finis
// OU si elles sont dans un autre namespace, par exemple :
// using Client.net.LunaPark.UI;

namespace Client.net.LunaPark
{
	public delegate void CheckboxChangeEvent(UIMenu sender, UIMenuCheckboxItem checkboxItem, bool Checked);
}
