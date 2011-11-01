﻿using System.Web;
using Glimpse.AspNet;
using Moq;
using Xunit;

namespace Glimpse.Test.AspNet
{
    public class HttpApplicationStateBaseDataAdapterShould
    {
        public Mock<HttpApplicationStateBase> ApplicationStateMock { get; set; }

        public HttpApplicationStateBaseDataAdapterShould()
        {
            var applicationStateMock = new Mock<HttpApplicationStateBase>();
            applicationStateMock.Setup(st => st.Set(It.IsAny<string>(), It.IsAny<object>()));
            applicationStateMock.Setup(st => st.Get(typeof(int).FullName)).Returns(5);
            ApplicationStateMock = applicationStateMock;
        }

        [Fact]
        public void ConstructWithAnHttpApplicationStateBase()
        {
            var dataStore = new HttpApplicationStateBaseDataStoreAdapter(ApplicationStateMock.Object);

            Assert.NotNull(dataStore);
        }

        [Fact]
        public void GetViaGenerics()
        {
            var dataStore = new HttpApplicationStateBaseDataStoreAdapter(ApplicationStateMock.Object);

            Assert.Equal(5, dataStore.Get<int>());
            ApplicationStateMock.Verify(st => st.Get(typeof(int).FullName), Times.Once());
        }

        [Fact]
        public void GetViaGenericsWithKey()
        {
            var dataStore = new HttpApplicationStateBaseDataStoreAdapter(ApplicationStateMock.Object);

            var result = dataStore.Get<int>(typeof(int).FullName);
            Assert.Equal(5, result);
            Assert.IsType<int>(result);
            ApplicationStateMock.Verify(st => st.Get(typeof(int).FullName), Times.Once());
        }

        [Fact]
        public void GetViaKey()
        {
            var dataStore = new HttpApplicationStateBaseDataStoreAdapter(ApplicationStateMock.Object);

            Assert.Equal(5, dataStore.Get(typeof(int).FullName));
            ApplicationStateMock.Verify(st => st.Get(typeof(int).FullName), Times.Once());
        }

        [Fact]
        public void SetViaGenerics()
        {
            var dataStore = new HttpApplicationStateBaseDataStoreAdapter(ApplicationStateMock.Object);
            
            dataStore.Set<int>(5);

            Assert.Equal(5, dataStore.Get<int>());
            ApplicationStateMock.Verify(st => st.Set(typeof(int).FullName, 5), Times.Once());

        }

        [Fact]
        public void SetViaKey()
        {
            var dataStore = new HttpApplicationStateBaseDataStoreAdapter(ApplicationStateMock.Object);
            
            dataStore.Set("aKey", "thing");
            ApplicationStateMock.Verify(st => st.Set("aKey", "thing"), Times.Once());

        }
    }
}