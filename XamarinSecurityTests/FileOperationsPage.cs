using System;

using Xamarin.Forms;
using PCLStorage;

namespace XamarinSecurityTests
{
	public class FileOperationsPage : ContentPage
	{
		string fileName = "somefile";
		public FileOperationsPage ()
		{
			Button buttonWrite = new Button { Text = "Write Test" };
			Button buttonRead = new Button { Text = "Read Test" };
			Button buttonDelete = new Button { Text = "Delete Test" };

			Label label = new Label ();

			StackLayout myLayout = new StackLayout { Padding = new Thickness (20, 80, 20, 20),
				Spacing = 60,
				Children = { buttonWrite, buttonRead, buttonDelete, label }
			};

			buttonWrite.Clicked += async (sender, e)=> {
				var file = await FileSystem.Current.LocalStorage.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
				var textToWrite = "this is a test";
				label.Text = "Writing to file " + fileName+ " with text:  " + textToWrite;
				await file.WriteAllTextAsync("this is a test");
			};

			buttonRead.Clicked += async (sender, e)=> {
				IFile file = null;
				try {
				 file = await FileSystem.Current.LocalStorage.GetFileAsync(fileName);
				 var content = await file.ReadAllTextAsync();
				 label.Text = "Reading from file: " + content;
				}catch{
					label.Text = "Could not find the file";
				}
			};

			//Just a little extra.
			buttonDelete.Clicked += async (sender, e)=> {
				IFile file = null;
				try {
					file = await FileSystem.Current.LocalStorage.GetFileAsync(fileName);
					label.Text = "Deleting file: " + fileName;
					await file.DeleteAsync();
				}catch{
					label.Text = "Could not find the file";
				}				
			};

			Content = myLayout;
		}
	}
}


