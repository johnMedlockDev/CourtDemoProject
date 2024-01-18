import PropTypes from 'prop-types'
import axios from 'axios'

const ChargeDetailPage = ({ charge }) => {
	return (
		<div>
			<h1>Charge Detail</h1>
			{charge
				? (
					<div>
						<p>Name: {charge.chargeName}</p>
						<p>Code: {charge.chargeCode}</p>
						<p>Type: {charge.chargeType}</p>
						<p>Judgement Type: {charge.judgementType}</p>
						<p>Fine Amount: {charge.fineAmount}</p>
						<p>Sentence Length (days): {charge.sentenceLengthIndays}</p>
					</div>
				)
				: (
					<p>Charge detail not found.</p>
				)}
		</div>
	)
}

export const getServerSideProps = async (context) => {
	const { id } = context.params

	try {
		const res = await axios.get(`http://api:8080/v1/Charges/${id}`)
		const charge = res.data // Adjust this according to the API response

		return {
			props: { charge }
		}
	} catch (error) {
		console.error('Error fetching charge detail:', error)
		return {
			props: { charge: null }
		}
	}
}

ChargeDetailPage.propTypes = {
	charge: PropTypes.shape({
		chargeId: PropTypes.string.isRequired,
		chargeName: PropTypes.string.isRequired,
		chargeCode: PropTypes.string.isRequired,
		chargeType: PropTypes.number.isRequired,
		judgementType: PropTypes.number.isRequired,
		fineAmount: PropTypes.number.isRequired,
		sentenceLengthIndays: PropTypes.number.isRequired
	})
}

export default ChargeDetailPage
