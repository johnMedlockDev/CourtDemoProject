import styles from '../../styles/pages/case-details/Details.module.scss'
import PropTypes from 'prop-types'
import axios from 'axios'
import Link from 'next/link'
import { useRouter } from 'next/router'

const CaseDetailsPage = ({ caseDetails }) => {
	const router = useRouter()

	const handleDelete = async (caseId) => {
		try {
			await axios.delete(`http://api:8080/v1/CaseDetails/${caseId}`)
			router.replace(router.asPath) // Refresh the page to update the list
		} catch (error) {
			console.error('Error deleting case:', error)
		}
	}

	return (
		<div>
			<h1>Case Details</h1>
			<Link href="/case-details/create"><a>Create New Case Detail</a></Link>
			<ul>
				{caseDetails.map((detail) => (
					<li key={detail.caseDetailId}>
						<Link href={`/case-details/${detail.caseDetailId}`}>
							<a>
								<p>Date: {new Date(detail.caseDetailEntryDateTime).toLocaleDateString()}</p>
								<p>Description: {detail.description}</p>
								<p>Docket Detail: {detail.docketDetail}</p>
								{detail.documentUri && <p>Document: <a href={detail.documentUri}>{detail.documentUri}</a></p>}
							</a>
						</Link>
						<button onClick={() => handleDelete(detail.caseDetailId)}>Delete</button>
					</li>
				))}
			</ul>
		</div>
	)
}

export const getServerSideProps = async () => {
	// Fetch data from your API
	const res = await axios.get('http://api:8080/v1/CaseDetails')
	const caseDetails = res.data // Adjust this according to the API response

	return {
		props: { caseDetails }
	}
}

CaseDetailsPage.propTypes = {
	caseDetails: PropTypes.arrayOf(
		PropTypes.shape({
			caseDetailId: PropTypes.string.isRequired,
			caseDetailEntryDateTime: PropTypes.string.isRequired,
			description: PropTypes.string,
			docketDetail: PropTypes.string,
			documentUri: PropTypes.string
		})
	).isRequired
}

export default CaseDetailsPage
