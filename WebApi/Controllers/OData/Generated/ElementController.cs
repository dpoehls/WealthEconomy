//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace forCrowd.WealthEconomy.WebApi.Controllers.OData
{
    using forCrowd.WealthEconomy.BusinessObjects;
    using forCrowd.WealthEconomy.Facade;
    using Results;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.OData;

    public abstract class BaseElementController : BaseODataController
    {
        public BaseElementController()
		{
			MainUnitOfWork = new ElementUnitOfWork();		
		}

		protected ElementUnitOfWork MainUnitOfWork { get; private set; }

        // GET odata/Element
        //[Queryable]
        public virtual IQueryable<Element> Get()
        {
			var list = MainUnitOfWork.AllLive;
            return list;
        }

        // GET odata/Element(5)
        //[Queryable]
        public virtual SingleResult<Element> Get([FromODataUri] int id)
        {
            return SingleResult.Create(MainUnitOfWork.AllLive.Where(element => element.Id == id));
        }

        // PUT odata/Element(5)
        public virtual async Task<IHttpActionResult> Put([FromODataUri] int id, Element element)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != element.Id)
            {
                return BadRequest();
            }

            try
            {
                await MainUnitOfWork.UpdateAsync(element);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await MainUnitOfWork.All.AnyAsync(item => item.Id == element.Id))
                {
                    return Conflict();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok(element);
        }

        // POST odata/Element
        public virtual async Task<IHttpActionResult> Post(Element element)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await MainUnitOfWork.InsertAsync(element);
            }
            catch (DbUpdateException)
            {
                if (await MainUnitOfWork.All.AnyAsync(item => item.Id == element.Id))
                {
					return new UniqueKeyConflictResult(Request, "Id", element.Id.ToString());
                }
                else throw;
            }

            return Created(element);
        }

        // PATCH odata/Element(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public virtual async Task<IHttpActionResult> Patch([FromODataUri] int id, Delta<Element> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var element = await MainUnitOfWork.AllLive.SingleOrDefaultAsync(item => item.Id == id);
            if (element == null)
            {
                return NotFound();
            }

            var patchEntity = patch.GetEntity();

            // TODO How is passed ModelState.IsValid?
            if (patchEntity.RowVersion == null)
                throw new InvalidOperationException("RowVersion property of the entity cannot be null");

            if (!element.RowVersion.SequenceEqual(patchEntity.RowVersion))
            {
                return Conflict();
            }

            patch.Patch(element);

            try
            {
                await MainUnitOfWork.UpdateAsync(element);
            }
            catch (DbUpdateException)
            {
                if (patch.GetChangedPropertyNames().Any(item => item == "Id"))
                {
                    object idObject = null;
                    patch.TryGetPropertyValue("Id", out idObject);

                    if (idObject != null && await MainUnitOfWork.All.AnyAsync(item => item.Id == (int)idObject))
                    {
                        return new UniqueKeyConflictResult(Request, "Id", idObject.ToString());
                    }
                    else throw;
                }
                else throw;
            }

            return Ok(element);
        }

        // DELETE odata/Element(5)
        public virtual async Task<IHttpActionResult> Delete([FromODataUri] int id)
        {
            var element = await MainUnitOfWork.AllLive.SingleOrDefaultAsync(item => item.Id == id);
            if (element == null)
            {
                return NotFound();
            }

            await MainUnitOfWork.DeleteAsync(element.Id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }

    public partial class ElementController : BaseElementController
    {
	}
}