using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DataMatrixPrinter.Common.Helpers
{
    public static class HtmlHelperExtensions
    {
        #region TextBox FormControl 

        public static MvcHtmlString HelperText<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.TextBoxFor(expression, new { @class = "form-control"});
        }

        public static MvcHtmlString HelperTextViaPlaceholder<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlName = html.LabelFor(expression).ToString();
            var start = htmlName.IndexOf(">", StringComparison.Ordinal);
            var end = htmlName.LastIndexOf("<", StringComparison.Ordinal);
            var name = htmlName.Substring(start + 1, end - start -1);
            return html.TextBoxFor(expression,
                new { @class = "form-control", placeholder = name });
        }

        public static MvcHtmlString HelperPassword<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.PasswordFor(expression, new { @class = "form-control" });
        }

        public static MvcHtmlString HelperPasswordViaPlaceholder<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlName = html.LabelFor(expression).ToString();
            var start = htmlName.IndexOf(">", StringComparison.Ordinal);
            var end = htmlName.LastIndexOf("<", StringComparison.Ordinal);
            var name = htmlName.Substring(start + 1, end - start - 1);
            return html.PasswordFor(expression,
                new { @class = "form-control", placeholder = name });
        }

        #endregion

        #region Label FormControl
        public static MvcHtmlString HelperLabel<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;

            return memberExpression == null ? null
                : html.LabelFor(expression, new { @for = memberExpression.Member.Name });
        }

        #endregion

        #region ValidationMessage FormControl
        public static MvcHtmlString HelperValidationMessage<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlString = "<span class='text-danger'>";
            htmlString += html.ValidationMessageFor(expression).ToString();
            htmlString += "</span>";
            return
                new MvcHtmlString(htmlString);
        }
        #endregion

        #region Button FormControl

        public static MvcHtmlString CustomButton(this HtmlHelper helper, string text, string id)
        {
            var htmlString = $"<button type='button' id='{id}' class='btn btn-primary'>" +
                             $"{text}" +
                             "<i class='icon-arrow-left13 position-right'></i></button>";
            return new MvcHtmlString(htmlString);
        }

        #endregion

        #region Select FormControl
        public static MvcHtmlString HelperSelect<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.DropDownListFor(expression, new List<SelectListItem>(), new { @class = "form-control select-search", style = "width: 100%" });
        }
        public static MvcHtmlString HelperSelect<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList)
        {
            return html.DropDownListFor(expression, selectList, new { @class = "form-control select-search", style = "width: 100%" });
        }
        public static MvcHtmlString HelperMultiSelect<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.DropDownListFor(expression, new List<SelectListItem>(), new { @class = "form-control select-search", multiple = "multiple", style = "width: 100%", data_placeholder = "لطفا ایتم های خود را انتخاب نمایید ..." });
        }
        public static MvcHtmlString HelperMultiSelect<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList)
        {
            return html.DropDownListFor(expression, selectList, new { @class = "form-control select-search", multiple = "multiple", style = "width: 100%", data_placeholder = "لطفا ایتم های خود را انتخاب نمایید ..." } );
        }
        public static MvcHtmlString HelperEnumSelect<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.EnumDropDownListFor(expression, null, new { @class = "form-control select2", style = "width: 100%" });
        }

        //todo : must replace with new combo version
        public static IHtmlString CustomDropDown(this HtmlHelper helper, IEnumerable<SelectListItem> selectlist, string id, string name)
        {
            string htmlString = $"<select class='select-search' id='{id}' name='{name}'>";
            foreach (var item in selectlist)
            {
                htmlString += $"<option value='{item.Value}'>{item.Text}</option>";
            }
            htmlString += "</select>";
            return new HtmlString(htmlString);
        }

        #endregion

        #region CheckBox FromControl
        public static MvcHtmlString HelperCheckBox<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, bool>> expression)
        {
            return html.CheckBoxFor(expression, new { @class = "styled" });
        }
        #endregion

        #region RadioButton FromControl
        public static MvcHtmlString HelperRadioButton<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, int>> expression,object value,bool isChecked)
        {
            return isChecked ? html.RadioButtonFor(expression, value, new { @class = "styled", Checked = "checked" }) : html.RadioButtonFor(expression, value, new { @class = "styled" });
        }
        #endregion

        #region TextArea FormControl
        public static MvcHtmlString HelperTextArea<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.TextAreaFor(expression, new { @class = "form-control" });
        }
        #endregion
    }
}