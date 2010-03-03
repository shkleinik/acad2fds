namespace GeometryConverter.BLL.Templates
{
    public interface ITemplateManager
    {
        string TemplatePath { get; set; }
        string TemplateName { get; set; }
        TemplateProvider  TemplateProvider{ get; set; }
    }
}