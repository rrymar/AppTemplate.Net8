using RestSharp;

namespace AppTemplate.Tests.Infrastructure
{
    public interface ICrudTestService<TModel, TId>
    {
        string Url { get; }

        TModel Create(TModel model);

        TModel Get(TId id);

        TModel Update(TId id, TModel model);

        void Delete(TId id);
    }
    
    public abstract class CrudTestService<TModel>(RestClient client) 
        : CrudTestService<TModel, int>(client);

    public abstract class CrudTestService<TModel, TId>(RestClient client) 
        : ICrudTestService<TModel, TId>
    {
        public abstract string Url { get; }

        private RestRequest CreateRequest(TId id)
        {
            return new RestRequest(Url + "/" + id);
        }

        public virtual TModel Get(TId id)
        {
            var request = CreateRequest(id);
            return client.Get<TModel>(request);
        }

        public virtual TModel Create(TModel model)
        {
            var request = new RestRequest(Url);
            request.AddBody(model);

            return client.Post<TModel>(request);
        }

        public virtual TModel Update(TId id, TModel model)
        {
            var request = CreateRequest(id);
            request.AddBody(model);

            return client.Put<TModel>(request);
        }

        public virtual void Delete(TId id)
        {
            var request = CreateRequest(id);
            client.Delete(request);
        }
    }
}
