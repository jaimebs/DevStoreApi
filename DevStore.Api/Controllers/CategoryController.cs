using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DevStore.Domain.Models;
using DevStore.Infra.DataContext;

namespace DevStore.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("apiMalaca")]
    public class CategoryController : ApiController
    {
        private readonly DevStoreDataContext _db = new DevStoreDataContext();

        [Route("categories")]
        public HttpResponseMessage GetCategoty()
        {
            var listCategory = _db.Categories.ToList();

            if (listCategory.Count == 0)
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "The list should have a category!");

            return Request.CreateResponse(HttpStatusCode.OK, listCategory);
        }

        [Route("category/{idCategory}/teste")]
        public HttpResponseMessage GetCategoryById(int idCategory)
        {
            try
            {
                var category = _db.Categories.Find(idCategory);
                if (category == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Category does not exist!");

                return Request.CreateResponse(HttpStatusCode.OK, category);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Fails to load category!");
            }
           
        }

        [HttpPost]
        [Route("category")]
        public HttpResponseMessage PostCategory(Category category)
        {
            if (category == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Enter with a category!");
            try
            {
                _db.Categories.Add(category);
                _db.SaveChanges();

                var result = category;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Fail to create Category!");
            }
        }
       
        [HttpPut]
        [Route("category")]
        public HttpResponseMessage PutCategory(Category category)
        {
            if (category == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Enter with a category!");
            try
            {
                _db.Entry(category).State = EntityState.Modified;
                _db.SaveChanges();

                var result = category;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Fail to update Category!");
            }
        }
       
        protected override void Dispose(bool disposing)
        {
           _db.Dispose();
        }

    }
}