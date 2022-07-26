using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CabInvoiceGeneratorTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// UC1-Return the total fare for normal ride
        /// </summary>
        [TestMethod]
        [TestCategory("CalculatingFare")]

        public void Return_TotalFare_ForNormalRide()
        {
            InvoiceGenerator invoice = new InvoiceGenerator(RideType.NORMAL_RIDE);
            double distance = 5.0;
            int time = 10;

            double fare = invoice.CalculateFare(distance, time);
            double expected = 60.0;
            Assert.AreEqual(expected, fare);

        }
        /// <summary>
        /// UC5-Return the total fare for premium ride
        /// </summary>
        [TestMethod]
        [TestCategory("CalculatingFare")]
        public void Return_TotalFare_ForPremiumRide()
        {

            InvoiceGenerator invoice = new InvoiceGenerator(RideType.PREMIUM_RIDE);
            double distance = 15.0;
            int time = 12;

            double fare = invoice.CalculateFare(distance, time);
            double expected = 237.0;
            Assert.AreEqual(expected, fare);

        }
        /// <summary>
        /// UC1&UC5-Handling the custom exception if distance is negative number or zero.
        /// </summary>
        [TestMethod]
        [TestCategory("CalculatingFare")]
        public void Return_TotalFare_ForRide_InvalidDistance()
        {
            string expected = "Distance should be positive integer";
            try
            {
                InvoiceGenerator invoice = new InvoiceGenerator(RideType.PREMIUM_RIDE);
                double distance = -15;
                int time = 12;
                double fare = invoice.CalculateFare(distance, time);

            }
            catch (CustomException ex)
            {
                Assert.AreEqual(expected, ex.message);
            }
        }
        /// <summary>
        /// UC1&UC5-Handling the custom exception if time is negative number or zero.
        /// </summary>
        [TestMethod]
        [TestCategory("CalculatingFare")]
        public void Return_TotalFare_ForRide_InvalidTime()
        {
            string expected = "Time should be positive integer";
            try
            {
                InvoiceGenerator invoice = new InvoiceGenerator(RideType.NORMAL_RIDE);
                double distance = 10;
                int time = 0;
                double fare = invoice.CalculateFare(distance, time);

            }
            catch (CustomException ex)
            {
                Assert.AreEqual(expected, ex.message);
            }
        }
        /// <summary>
        /// UC2-Returns the totlfare for multiple rides
        /// </summary>
        [TestMethod]
        public void Return_Multiple_Rides_TotalFare()
        {
            InvoiceGenerator invoice = new InvoiceGenerator(RideType.PREMIUM_RIDE);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };

            InvoiceSummary summary = new InvoiceSummary(2, 55.0);
            InvoiceSummary expected = invoice.CalculateFare(rides);
            Assert.AreEqual(summary.totalFare, expected.totalFare);
        }
        /// <summary>
        /// UC3-Returns the average ride
        /// </summary>
        [TestMethod]
        public void Return_Multiple_Rides_AverageFare()
        {
            InvoiceGenerator invoice = new InvoiceGenerator(RideType.PREMIUM_RIDE);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };

            InvoiceSummary summary = new InvoiceSummary(2, 55.0);
            InvoiceSummary expected = invoice.CalculateFare(rides);
            Assert.AreEqual(summary.avgFare, expected.avgFare);
        }
        /// <summary>
        /// UC3-returns the number of rides
        /// </summary>
        [TestMethod]
        public void Return_Multiple_Rides_NumofRides()
        {
            InvoiceGenerator invoice = new InvoiceGenerator(RideType.PREMIUM_RIDE);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };

            InvoiceSummary summary = new InvoiceSummary(2, 55.0);
            InvoiceSummary expected = invoice.CalculateFare(rides);
            Assert.AreEqual(summary.numOfRides, expected.numOfRides);
        }
        /// <summary>
        /// UC4-Adding the data in the list correponding userid and rides
        /// </summary>
        [TestMethod]
        public void Given_UserId_Return_Fare()
        {
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 1) };
            RideRepository rideRepository = new RideRepository();
            rideRepository.AddRide(1, rides);
            rideRepository.AddRide(2, rides);
            var rideArray = rideRepository.GetRides(1);
            InvoiceGenerator invoice = new InvoiceGenerator(RideType.NORMAL_RIDE);

            InvoiceSummary summary = new InvoiceSummary(2, 30.0);
            InvoiceSummary expected = invoice.CalculateFare(rideArray);
            Assert.AreEqual(summary.totalFare, expected.totalFare);
        }
    }
}