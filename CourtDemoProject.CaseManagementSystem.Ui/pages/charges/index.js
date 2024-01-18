import styles from '../../styles/pages/charges/Charges.module.scss'
import PropTypes from 'prop-types'
import axios from 'axios'
import Link from 'next/link'
import { useRouter } from 'next/router'

const ChargesPage = ({ charges }) => {
	const router = useRouter()

	const handleDelete = async (caseId) => {
		try {
			await axios.delete(`http://api:8080/v1/Charges/${caseId}`)
			router.replace(router.asPath) // Refresh the page to update the list
		} catch (error) {
			console.error('Error deleting case:', error)
		}
	}

	return (
		<div>
			<h1>Charges</h1>
			<Link href="/charges/create"><a>Create New Charge</a></Link>
			<ul>
				{charges.map((charge) => (
					<li key={charge.chargeId}>
						<Link href={`/charges/${charge.chargeId}`}>
							<a>
								<p>Charge Name: {charge.chargeName}</p>
								<p>Charge Code: {charge.chargeCode}</p>
							</a>
						</Link>
						<button onClick={() => handleDelete(charge.chargeId)}>Delete</button>
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
