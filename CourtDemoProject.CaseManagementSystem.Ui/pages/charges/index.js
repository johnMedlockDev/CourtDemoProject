import PropTypes from 'prop-types'
import axios from 'axios'
import Link from 'next/link'

const ChargesPage = ({ charges }) => {
	return (
		<div>
			<h1>Charges</h1>
			<ul>
				{charges.map((charge) => (
					<li key={charge.chargeId}>
						<Link href={`/charges/${charge.chargeId}`}>
							<a>
								<p>Charge Name: {charge.chargeName}</p>
								<p>Charge Code: {charge.chargeCode}</p>
							</a>
						</Link>
					</li>
				))}
			</ul>
		</div>
	)
}

export const getServerSideProps = async () => {
	const res = await axios.get('http://api:8080/v1/Charges')
	const charges = res.data // Adjust this according to the API response

	return {
		props: { charges }
	}
}

ChargesPage.propTypes = {
	charges: PropTypes.arrayOf(
		PropTypes.shape({
			chargeId: PropTypes.string.isRequired,
			chargeName: PropTypes.string.isRequired,
			chargeCode: PropTypes.string.isRequired
		})
	).isRequired
}

export default ChargesPage
