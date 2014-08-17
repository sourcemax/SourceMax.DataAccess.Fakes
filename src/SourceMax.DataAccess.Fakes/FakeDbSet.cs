using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace SourceMax.DataAccess.Fakes {

    public class FakeDbSet<T> : DbSet<T>, IQueryable, IEnumerable<T>, IDbAsyncEnumerable<T>, IDbSet<T> where T : class {

        private ObservableCollection<T> Data { get; set; }

        //private IQueryable Queryable { get; set; }

        public FakeDbSet() {
            this.Data = new ObservableCollection<T>();
            //this.Queryable = this.Data.AsQueryable();
        }

        public FakeDbSet(T item) : this() {
            this.Add(item);
        }

        public FakeDbSet(IEnumerable<T> items) : this() {
            items.ToList().ForEach(item => this.Add(item));
        }

        public override T Find(params object[] keyValues) {
            throw new NotImplementedException("Not implemented, must override FakeDbSet<T> and implement the Find method.");
        }

        public override T Add(T item) {
            this.Data.Add(item);
            return item;
        }

        public override T Remove(T item) {
            this.Data.Remove(item);
            return item;
        }

        public override T Attach(T item) {
            this.Data.Add(item);
            return item;
        }

        public void Detach(T item) {
            this.Data.Remove(item);
        }

        Type IQueryable.ElementType {
            get { 
                return this.Data.AsQueryable().ElementType; 
            }
        }

        Expression IQueryable.Expression {
            get { 
                return this.Data.AsQueryable().Expression; 
            }
        }

        IQueryProvider IQueryable.Provider {
            get { 
                return new TestDbAsyncQueryProvider<T>(this.Data.AsQueryable().Provider); 
            } 
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.Data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return this.Data.GetEnumerator();
        }

        public override T Create() {
            return Activator.CreateInstance<T>();
        }

        IDbAsyncEnumerator<T> IDbAsyncEnumerable<T>.GetAsyncEnumerator() {
            return new TestDbAsyncEnumerator<T>(this.Data.GetEnumerator());
        } 

        public override ObservableCollection<T> Local {
            get {
                return this.Data;
            }
        }

        public override TDerivedEntity Create<TDerivedEntity>() {
            return Activator.CreateInstance<TDerivedEntity>();
        }
    }
}
