using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SourceMax.DataAccess.Fakes {

    public class FakeDbSet<T> : IDbSet<T> where T : class {

        private HashSet<T> Data { get; set; } 

        public FakeDbSet() {
            this.Data = new HashSet<T>();
        }

        public FakeDbSet(T item) : this() {
            this.Add(item);
        }

        public FakeDbSet(IEnumerable<T> items) : this() {
            items.ToList().ForEach(item => this.Add(item));
        }

        public virtual T Find(params object[] keyValues) {
            throw new NotImplementedException("Not implemented, must override FakeDbSet<T> and implement the Find method.");
        }

        public T Add(T item) {
            this.Data.Add(item);
            return item;
        }

        public T Remove(T item) {
            this.Data.Remove(item);
            return item;
        }

        public T Attach(T item) {
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
                return this.Data.AsQueryable().Provider; 
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.Data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return this.Data.GetEnumerator();
        }

        public T Create() {
            return Activator.CreateInstance<T>();
        }

        public ObservableCollection<T> Local {
            get {
                return new ObservableCollection<T>(this.Data);
            }
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T {
            return Activator.CreateInstance<TDerivedEntity>();
        }
    }
}
