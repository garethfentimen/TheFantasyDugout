Imports System
Imports System.Security
Imports System.Security.Principal
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing
Imports System.Web.Security
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports LaGiga

Namespace TheListTests

    '''<summary>
    '''This is a test class for TheListIndexModelTest and is intended
    '''to contain all TheListIndexModelTest Unit Tests
    '''</summary>
    <TestClass()> _
    Public Class TestGetNextDivisibleBy10

        <TestMethod()>
        Public Sub GreaterThan10ButLessThan20()

            Dim paging = New PagingCalculator(11, 100)

            Assert.AreEqual(20, paging.HighPageLimit)
            Assert.AreEqual(11, paging.LowPageLimit)

        End Sub

        <TestMethod()>
        Public Sub ExactlyOnTheLimit()

            Dim paging = New PagingCalculator(10, 100)

            Assert.AreEqual(10, paging.HighPageLimit)
            Assert.AreEqual(1, paging.LowPageLimit)

        End Sub

        <TestMethod()>
        Public Sub ExactlyOnTheLimitGreaterThan10()

            Dim paging = New PagingCalculator(30, 100)

            Assert.AreEqual(30, paging.HighPageLimit)
            Assert.AreEqual(21, paging.LowPageLimit)

        End Sub

        <TestMethod()>
        Public Sub LessThan10()

            Dim paging = New PagingCalculator(1, 100)

            Assert.AreEqual(10, paging.HighPageLimit)
            Assert.AreEqual(1, paging.LowPageLimit)

        End Sub

        <TestMethod()>
        Public Sub IfPageIsCurrently0()

            Dim paging = New PagingCalculator(0, 100)

            Assert.AreEqual(10, paging.HighPageLimit)
            Assert.AreEqual(1, paging.LowPageLimit)

        End Sub

        <TestMethod()>
        Public Sub IfPageIsInTheLast10()

            Dim paging = New PagingCalculator(95, 97)

            Assert.AreEqual(97, paging.HighPageLimit)
            Assert.AreEqual(91, paging.LowPageLimit)

        End Sub

    End Class

End Namespace
