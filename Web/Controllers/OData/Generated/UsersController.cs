//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace forCrowd.WealthEconomy.Web.Controllers.OData
{
    using forCrowd.WealthEconomy.BusinessObjects;
    using forCrowd.WealthEconomy.Facade;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.OData;

    public abstract class BaseUsersController : BaseODataController
    {
        public BaseUsersController()
		{
			MainUnitOfWork = new UserUnitOfWork();		
		}

		protected UserUnitOfWork MainUnitOfWork { get; private set; }

        // GET odata/User
        //[Queryable]
        public virtual IQueryable<User> Get()
        {
			var list = MainUnitOfWork.AllLive;
            return list;
        }

        // GET odata/User(5)
        //[Queryable]
        public virtual SingleResult<User> Get([FromODataUri] int key)
        {
            return SingleResult.Create(MainUnitOfWork.AllLive.Where(user => user.Id == key));
        }

        // PUT odata/User(5)
        public virtual async Task<IHttpActionResult> Put([FromODataUri] int key, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != user.Id)
            {
                return BadRequest();
            }

            try
            {
                await MainUnitOfWork.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainUnitOfWork.Exists(key))
                {
                    return NotFound();
                }
                else
                {
                    return Conflict();
                }
            }

            return Ok(user);
        }

        // POST odata/User
        public virtual async Task<IHttpActionResult> Post(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await MainUnitOfWork.InsertAsync(user);
            }
            catch (DbUpdateException)
            {
                if (MainUnitOfWork.Exists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(user);
        }

        // PATCH odata/User(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public virtual async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<User> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await MainUnitOfWork.AllLive.SingleOrDefaultAsync(item => item.Id == key);
            if (user == null)
            {
                return NotFound();
            }

            var patchEntity = patch.GetEntity();

            // TODO How is passed ModelState.IsValid?
            if (patchEntity.RowVersion == null)
                throw new InvalidOperationException("RowVersion property of the entity cannot be null");

            if (!user.RowVersion.SequenceEqual(patchEntity.RowVersion))
            {
                return Conflict();
            }

            patch.Patch(user);
            await MainUnitOfWork.UpdateAsync(user);

            return Ok(user);
        }

        // DELETE odata/User(5)
        public virtual async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var user = await MainUnitOfWork.AllLive.SingleOrDefaultAsync(item => item.Id == key);
            if (user == null)
            {
                return NotFound();
            }

            await MainUnitOfWork.DeleteAsync(user.Id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }

    public partial class UsersController : BaseUsersController
    {
	}
}
