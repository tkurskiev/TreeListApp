using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using TreeListApp.Exceptions;

namespace TreeListApp
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();

            this.Load += OnLoad;

            treeList1.DataSource = GetViewModels();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (sender is XtraForm form)
                form.Text = @"TreeViewApp";
        }

        private IEnumerable<ViewModel> GetViewModels()
        {
            try
            {
                //var data = CatalogLevelDataService.GetAllTreeListDataObjects();

                var viewModels = CatalogLevelDataService.GetAllTreeListDataObjects().Select(x => new ViewModel(x));

                return viewModels;
            }
            catch (DbException dbException)
            {
                dbException.ShowMessage();
                return null;
            }
        }

        private class ViewModel
        {
            #region Public Properties

            public int Id { get; set; }

            public int? ParentId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            #endregion

            #region Public Constructor

            public ViewModel(TreeListDto dto)
            {
                Id = dto.Id;

                ParentId = dto.ParentId;

                Name = dto.Name;

                Description = dto.Description;
            }

            #endregion
        }
    }
}
