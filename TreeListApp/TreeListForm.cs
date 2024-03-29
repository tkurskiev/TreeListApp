﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using TreeListApp.DataService;
using TreeListApp.Exceptions;

namespace TreeListApp
{
    public partial class TreeListForm : DevExpress.XtraEditors.XtraForm
    {
        #region Private Fields

        private readonly ICatalogLevelDataService _catalogLevelDataService = new CatalogLevelDataService();

        #endregion

        #region Public Constructors
        
        public TreeListForm()
        {
            InitializeComponent();

            this.Load += OnLoad;

            //var dataSource = GetViewModels().ToList();
            var dataSource = new List<ViewModel>();

            //treeList1.ChildListFieldName
            treeList1.KeyFieldName = nameof(ViewModel.Id);
            treeList1.ParentFieldName = nameof(ViewModel.ParentId);
            treeList1.DataSource = dataSource;
        }

        #endregion

        #region Private Helpers

        private void OnLoad(object sender, EventArgs e)
        {
            if (sender is XtraForm form)
                form.Text = @"TreeViewApp";


            Task.Factory.StartNew(GetViewModels).ContinueWith(task =>
            {
                treeList1.DataSource = task.Result.ToList();
                treeList1.RefreshDataSource();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private IEnumerable<ViewModel> GetViewModels()
        {
            try
            {
                var viewModels = _catalogLevelDataService.GetAllTreeListDataObjects().Select(x => new ViewModel(x));

                return viewModels;
            }
            catch (DbException dbException)
            {
                dbException.ShowMessage();
                return null;
            }
        }

        //private async void UpdateDataSource()
        //{
        //    var result = await GetViewModelsAsync();
            
        //    treeList1.DataSource = result;
        //}

        //private Task<IEnumerable<ViewModel>> GetViewModelsAsync()
        //{
        //    return Task.Factory.StartNew(GetViewModels);
        //}

        #endregion

        #region Classes

        private class ViewModel
        {
            #region Public Properties

            public int Id { get; set; }

            public int ParentId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            #endregion

            #region Public Constructor

            public ViewModel(TreeListDto dto)
            {
                Id = dto.Id;

                ParentId = dto.ParentId ?? 0;

                Name = dto.Name;

                Description = dto.Description;
            }

            #endregion
        }

        #endregion
    }
}
